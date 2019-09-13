
#include "msp430fr5969.h"
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <math.h>

#define fclk 16000000

int start = 0, record = 0, ADC_ch1, ADC_ch2, GND, filt, flag = 0, ang, filt1;
unsigned int d, m, loss_byte = 10, loss_byte1;
unsigned long int fd = 0;

#define M_50 3 //порядок + 1
#define M_pas 5
#define gain 0.000037228449408321616


/*static const double b[3] = {0.000009832797264287646, 0.000019665594528575292,  0.000009832797264287646};
static const double a[3] = {1.0, -1.991111174841077,  0.99115050603013399};*/

static const double b[3] = {1, 2,  1};
static const double a[3] = {1.0, -1.98266802151097, 0.9828169353086};

/*static const double b2[M_pas] = { 0.8657945032570078,  0.0000000000000004, -1.7315890065140156, -0.0000000000000004, 0.8657945032570078};
static const double a2[M_pas] = {1.0, -0.28474287138797849, -1.6777536304957306,   0.21281590847449627, 0.74968196061709269};
*/

/*double x1[M_50], y1[M_50], x2[M_50], y2[M_50];
double x3[M_pas], y3[M_pas];*/
double x[3],y[3];

int smooth(int samp) { //сглаживающий фильтр
	for (m = 2; m > 0; m--)
		    {
		      y[m] = y[m-1];
		      x[m] = x[m-1];
		    }
		    x[0] =samp*gain;
		    y[0] = x[0]*b[0] + x[1]*b[1] + x[2]*b[2] - y[1]*a[1] - y[2]*a[2];

		    return (int)y[0];
}

inline void transmit(unsigned int a) { //ф-ия передачи числа

	while(!(UCA0IFG&UCTXIFG));
	UCA0TXBUF = a;
	while(!(UCA0IFG&UCTXIFG));
	a = a >> 8;
	UCA0TXBUF = a;
}


int main(void) {

    WDTCTL = WDTPW | WDTHOLD;	// Stop watchdog timer

    FRCTL0 = FRCTLPW | NACCESS_1;

    CSCTL0_H = CSKEY >> 8;                    // Unlock clock registers
    CSCTL1 = DCORSEL | DCOFSEL_4;             // Set DCO to 16MHz
    CSCTL2 = SELA__VLOCLK | SELS__DCOCLK | SELM__DCOCLK;
    CSCTL3 = DIVA__1 | DIVS__1 | DIVM__1;     // Set all dividers
    CSCTL0_H = 0;                             // Lock CS registers

    PM5CTL0 = 0;

    P1DIR = BIT0;
    P1REN = BIT0;
    P1OUT &= ~BIT0;
    P4DIR = BIT6;
    P4OUT = BIT5;
    P4REN = BIT5;
    P4IES = BIT5;
    P4IE = BIT5;

    P2SEL1 = BIT0 + BIT1;		//настройка выводов UART
    P2SEL0 &= ~(BIT0 | BIT1);

    P1SEL1 |= BIT3;
    P1SEL0 |= BIT3;
    P2SEL1 |= BIT4;			//настройка выводов АЦП
    P2SEL0 |= BIT4;
    P3SEL1 |= BIT0;
    P3SEL0 |= BIT0;
    P4SEL1 |= BIT2 + BIT3;
    P4SEL0 |= BIT2 + BIT3;

    PM5CTL0 &= ~LOCKLPM5;

    P4IFG &=~BIT5;

    	//для ПК****************************************************************************
    UCA0CTLW0 = UCSWRST;                      // Put eUSCI in reset
    UCA0CTLW0 |= UCSSEL__SMCLK;               // CLK = SMCLK
    UCA0BR0 |= 62;                            // 256000 bps
    UCA0BR1 |= 0;
    UCA0MCTLW_H |= 0xAA;	//0x55
    UCA0MCTLW_L |= 0x00; //UCBRF_10 | UCOS16;
    UCA0CTLW0 &= ~UCSWRST;                    // Initialize eUSCI
    UCA0IE |= UCRXIE;                      // Enable USCI_A0 RX interrupt

    	//для Android*******************************************************************************
    /*UCA0CTLW0 = UCSWRST;                      // Put eUSCI in reset
    UCA0CTLW0 |= UCSSEL__SMCLK;               // CLK = SMCLK
    UCA0BRW = 8;								//115200 bps
    UCA0MCTLW_H |= 0xF7;	//UCBRSx
    UCA0MCTLW_L |= UCBRF_10 + UCOS16;
    UCA0CTLW0 &= ~UCSWRST;                    // Initialize eUSCI
    UCA0IE |= UCRXIE;   */                      // Enable USCI_A0 RX interrupt

    //TA0CCTL0 |= OUTMOD_3;	//CCIE
    TA0CCTL1 |= OUTMOD_3;
    TA0CCR0 |= 4000;			//частота дискретизации
    TA0CCR1 |= TA0CCR0 - 1;
    TA0CTL |= TASSEL_2 + MC_1 + ID_0 + TACLR; //SMCLK, UP to CCR0, DIV 2,

    ADC12CTL0 &= ~ADC12ENC;
    ADC12CTL0 |= ADC12ON + ADC12SHT0_0;
    ADC12CTL1 |= ADC12SHS_1 + ADC12SHP + ADC12SSEL_3 + ADC12CONSEQ_3;//ADC12CONSEQ_2;
    ADC12CTL2 |= ADC12RES_1;
    //ADC12CTL3 |= ADC12CSTARTADD0; //начало последовательности преобразований
    //ADC12CTL2 |= ADC12DF;		//знаковое представление кода
    ADC12MCTL0 |= ADC12INCH_7;
    ADC12MCTL1 |= ADC12INCH_11 + ADC12EOS;
    //ADC12MCTL0 |= ADC12DIF;
    ADC12IER0 |= ADC12IE0;
    //ADC12CTL0 ^= ADC12ENC;

    __bis_SR_register(GIE); //разрешить глобальные прерывания

    while(1){

    	//__bis_SR_register(LPM3_bits + GIE);
    	//__no_operation();


    	if (start > loss_byte) {

    		transmit(ADC_ch1);

    		transmit(ADC_ch2);

    		start = 0;

    	}
    	//else start++;

    }
}

#pragma vector=PORT4_VECTOR
__interrupt void Port_4(void) //вкл/выкл АЦП
{                  // Toggle P1.6
   P4IFG &=~BIT5;                        // P1.3 IFG cleared
   P1OUT ^= BIT0;
   ADC12CTL0 ^= ADC12ENC;
   /*TA0CTL &=~TAIFG;
   TA0CCTL0 &=~ CCIFG;*/

   __delay_cycles(400000);
   __no_operation();
}



#pragma vector=USCI_A0_VECTOR //настройка работы МК
__interrupt void UART_ISR(void) // через ПК
{
	char str[20];
	unsigned int i = 0;

	switch(UCA0IFG && 0xFF)
	{
		case UCRXIFG: //прием строки

			do{
				while(!(UCA0IFG&UCRXIFG));
				str[i] = UCA0RXBUF;
				i++;
			}while(str[i-1] != '\n');



			switch(str[0]) //определение назначения
			{			//строки и извлечение значения
			case 'l':	//прореживание
				P1OUT ^= BIT0;
				//loss_byte = atoi(&str[1]);
				loss_byte = strtoul(&str[1], NULL, 10);
				loss_byte1 = loss_byte;
				break;
			case 'f':	//частота дискретизации
				P4OUT ^= BIT6;
				fd = strtoul(&str[1], NULL, 10);
				int dv;
				dv = TA0CTL & ID_3;
				dv = dv >> 6;
				dv = pow(2, dv);
				//TA0CCR0 = 8000000/fd;		//частота дискретизации
				TA0CCR0 = (fclk/dv)/fd/2;	//делить на два при двух каналах
				fd = TA0CCR0;
				TA0CCR1 = TA0CCR0 - 1;

			    break;
			case 'r': //запись сигнала
				if(str[1] == 'n') {
					record = 1;
					loss_byte = 1;
				}
				else {
					record = 0;
					loss_byte = loss_byte1;
				}
			default: break;
			}

			UCA0IFG &= ~UCRXIFG;
			break;
		default: break;
	}
}


#pragma vector = ADC12_VECTOR
__interrupt void ADC12_ISR(void)

{
	switch (__even_in_range(ADC12IV, ADC12IV_ADC12RDYIFG))
	{
		case ADC12IV_NONE:        break;        // Vector  0:  No interrupt
		case ADC12IV_ADC12OVIFG:  break;        // Vector  2:  ADC12MEMx Overflow
		case ADC12IV_ADC12TOVIFG: break;        // Vector  4:  Conversion time overflow
		case ADC12IV_ADC12HIIFG:  break;        // Vector  6:  ADC12BHI
		case ADC12IV_ADC12LOIFG:  break;        // Vector  8:  ADC12BLO
		case ADC12IV_ADC12INIFG:  break;        // Vector 10:  ADC12BIN
		case ADC12IV_ADC12IFG0:

			ADC_ch1 = ADC12MEM0;
			ADC_ch2 = ADC12MEM1;
			start++;

			ADC12IFGR0 = 0;
			//ADC12IV = 0;

			/*__bic_SR_register_on_exit(LPM3_bits);
			__no_operation();*/

			break;        						// Vector 12:  ADC12MEM0 Interrupt
		case ADC12IV_ADC12IFG1:   break;		// Vector 14:  ADC12MEM1 Interrupt
		case ADC12IV_ADC12IFG2:   break;        // Vector 16:  ADC12MEM2
		case ADC12IV_ADC12IFG3:   break;        // Vector 18:  ADC12MEM3
		case ADC12IV_ADC12IFG4:   break;        // Vector 20:  ADC12MEM4
		case ADC12IV_ADC12IFG5:   break;        // Vector 22:  ADC12MEM5
		case ADC12IV_ADC12IFG6:   break;        // Vector 24:  ADC12MEM6
		case ADC12IV_ADC12IFG7:   break;        // Vector 26:  ADC12MEM7
		case ADC12IV_ADC12IFG8:   break;        // Vector 28:  ADC12MEM8
		case ADC12IV_ADC12IFG9:   break;        // Vector 30:  ADC12MEM9
		case ADC12IV_ADC12RDYIFG: break;        // Vector 76:  ADC12RDY
		default: break;
	  }
}
