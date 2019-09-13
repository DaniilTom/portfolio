<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<title>Генератор случайных паролей</title>
	<link rel="stylesheet" href="style.css">

	<script type="text/javascript">
		
		function generator(){
			var num = +document.getElementById("numNumber").value;

			var symbol = "";
			for(var i = 0; i < num; i++){
				
				var charOrDigit = parseInt(Math.random() * 10);
				if(charOrDigit > 5){ // если charOrDigit равно нулю, сгенерировать букву

					//приходится разделять выбор заглавной и строчной буквы, т.к. в таблице utf-8
						//они находятся в разных местах
					var capitalOrLowercase =  parseInt(Math.random()*10);

					// 26 английских букв, смещение в таблице utf-8 на 65
					if(capitalOrLowercase > 5) symbol += String.fromCharCode(parseInt(Math.random()*26) + 65);
					else symbol += String.fromCharCode(parseInt(Math.random()*26) + 97); //97 - смещение на строчные

				}
				else {
					symbol += parseInt(Math.random()*9); // цифры от 0 до 9
				}

			}

			document.getElementById("info").innerHTML = "Ваш пароль: " + symbol;
		}

	</script>
</head>
<body>
	<div class="content">
		<?php include "menu.php"; ?>

		<div class="content">
			<div class="centre">
			<h1>Генератор случайных паролей</h1>

				<div class="box">
					<p id="info">Введите количество знаков: </p>
					<input type="text" id="numNumber">
					<br>
					<a href="#" onClick="generator();" id="button">Начать</a>
				</div>
			</div>
		</div>
		
	</div>
	<div class="footer">
		Copyright &copy; <?php echo date("Y"); ?>Daniil<div>
	<div>
</body>
</html>