<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<title>Личный сайт студента GeekBrains</title>
	<link rel="stylesheet" href="style.css">

	<script type="text/javascript">

		var num = parseInt(Math.random() * 100);
		var tryCount = 0;
		//var maxTryCount = 3;
		var turn = 0;

		function readInt(){
			return +document.getElementById("userAnswer").value;
		}

		function write(text,  id){
			document.getElementById(id).innerHTML = text;
		}

		function hide(id){
			document.getElementById(id).style.display = "none";
		}
		
		function guess(){

			/*if(i%2 == 0)
			{
				var answer = prompt("Очередь игрока № 1: ");
			}
			else var answer = prompt("Очередь игрока № 2: ");*/
			tryCount++;

			var answer = readInt();

			if(parseInt(answer) == num && !isNaN(parseInt(answer))) {
				write("<b>Вы победили.</b>", "info");
				hide("button");
				hide("userAnswer");
				return;
			}
			/*else if(tryCount >= maxTryCount){
				write("<b>Вы проиграли.</b> Правильный ответ: " + num, "info");
				hide("button");
				hide("userAnswer");
				return;
			}*/
			//else if(answer.toLowerCase() == "exit") 	  {write("<b>Вы проиграли.</b>"); return;}

			if(parseInt(answer) > num) write("Введенное число больше.", "info");
			else write("Введенное число меньше.", "info");

			turn++;
			if(turn%2 == 0) write("Сечас ходит <b><i>игрок №1</i></b>", "playerTurn");
			else write("Сечас ходит <b><i>игрок №2</i></b>", "playerTurn");
		}

	</script>
</head>
<body>

<div class="content">
	<?php include "menu.php"; ?>

	<div class="content">
		<div class="centre">
		<h1>Игра угадайка</h1>

			<div class="box">
				<p id="info">Угадайте число от 0 до 100</p>
				<p id="playerTurn">Сечас ходит <b><i>игрок №1</i></b></p>
				<input type="text" id="userAnswer">
				<br>
				<a href="#" onClick="guess();" id="button">Начать</a>
			</div>
		</div>
	</div>
	
</div>
<div class="footer">
	Copyright &copy; <?php echo date("Y"); ?>Daniil<div>
<div>


</body>
</html>