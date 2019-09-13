<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<title>Личный сайт студента GeekBrains</title>
	<link rel="stylesheet" href="style.css">

</head>
<body>

<div class="content">
	<?php include "menu.php" ?>

	<div class="content">
		<div class="centre">
		<h1>Игра в загадки</h1>

			<div class="box">


				<?php

					if(isset($_GET['userAnswer1']) && isset($_GET['userAnswer2']) && isset($_GET['userAnswer3']))
					{
						$userAnswer = $_GET["userAnswer1"];
						$score = 0;
						if(strtolower($userAnswer) == "солнце"){
							$score++;
						}

						$userAnswer = $_GET["userAnswer2"];
						if(strtolower($userAnswer) == "ш"){
							$score++;
						}

						$userAnswer = $_GET["userAnswer3"];
						if(strtolower($userAnswer) == "печкин"){
							$score++;
						}

						echo "Вы угадали ". $score . " загадок";
					}

				?>

				<form method="GET">
					<p>Над рекой остановился<br>Шар воздушный, золотой.<br>А потом за лесом скрылся,<br>Покачавшись над водой.</p>
					<input type="text" name="userAnswer1">
					
					<p>Для шипенья хороша<br>В алфавите буква…</p>
					<input type="text" name="userAnswer2">
					
					<p>В Простоквашино живёт,<br>Службу там свою несёт.<br>Почта-дом стоит у речки.<br>Почтальон в ней — дядя…</p>
					<input type="text" name="userAnswer3">
					<br>
					<input type="submit" value="Ответить">
				</form>
			</div>
		</div>
	</div>
	
</div>
<div class="footer">
	Copyright &copy; <?php echo date("Y"); ?>Daniil<div>
<div>


</body>
</html>