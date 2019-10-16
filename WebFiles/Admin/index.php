<!DOCTYPE html>
<html>
<title>Hyper Hook</title>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1">

<!-- Style -->

<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="../Files/css/w3.css">

<style>
body, html {height: 100%}
.bgimg {
    background-image: url('../Files/images/Background.jpg');
    min-height: 100%;
    background-position: center;
    background-size: cover;
}

form {
    border: 0px
}

input[type=text], select {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #4B0082;
  border-radius: 4px;
  box-sizing: border-box;
}

input[type=Password], select {
  width: 100%;
  padding: 12px 20px;
  margin: 8px 0;
  display: inline-block;
  border: 1px solid #4B0082;
  border-radius: 4px;
  box-sizing: border-box;
}

button {
  width: 100%;
  background-color: #4B0082;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  opacity: 0.8;
}

.modal {
  display: none;
  position: fixed;
  z-index: 1;
  padding-top: 100px;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  overflow: auto;
  background-color: rgb(0,0,0);
  background-color: rgba(0,0,0,0.4);
}

.modal-content {
  background-color: #fefefe;
  margin: auto;
  padding: 20px;
  border: 1px solid #888;
  width: 80%;
}

.close {
  color: #aaaaaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: #000;
  text-decoration: none;
  cursor: pointer;
}

.purple_neat {
  color: #fff!important;
  background-color: #4B0082!important;
}

</style>

<!-- Site code -->

<body>

<div class="bgimg w3-display-container w3-animate-opacity w3-text-white">
  <div class="w3-display-middle">

    <iframe name="hiddenFrame" width="0" height="0" border="0" style="display: none;"></iframe>

    <form action="<?php $_SERVER['PHP_SELF'];?>" method="POST">
      <label for="Username"><b>Username</b></label>
      <input type="text" id="UsernameBox" placeholder="Enter Username" name="Username" required>

      <label for="Password"><b>Password</b></label>
      <input type="Password" id="PasswordBox" placeholder="Enter Password" name="Password" required>

      <label for="Amount">Amount</label>
      <select id="AmountBox" name="Amount">
        <option value=1>1 Key(s)</option>
        <option value=5>5 Key(s)</option>
        <option value=10>10 Key(s)</option>
        <option value=50>50 Key(s)</option>
      </select>

      <label for="Expires">Expires</label>
      <select id="TimeBox" name="Expires">
        <option value=1>1 Day(s)</option>
        <option value=7>7 Day(s)</option>
        <option value=30>30 Day(s)</option>
        <option value=25000000>Lifetime</option>
      </select>

      <label for="Beta">Beta Access</label>
      <select id="BetaBox" name="Beta">
        <option value=0>False</option>
        <option value=1>True</option>
      </select>

      <button id="SubmitButton" onclick="">Submit</button>

      <div id="id01" class="modal">

        <div class="w3-modal-content">
          <header class="w3-container w3-display-topcenter purple_neat">
            <span onclick="document.getElementById('id01').style.display='none'"
              class="w3-button w3-display-topright">&times;</span>
              <h2>License key generator</h2>
            </header>
            <div class="w3-container">

              <span style="color:black;">

                <?php

                # Get variables from post method

                $Username   = $_POST['Username'];
                $Password   = $_POST['Password'];
                $Amount     = $_POST['Amount'];
                $Expires    = $_POST['Expires'];
                $Betastatus = $_POST['Beta'];

                # Static variables

                $Inifile = $_SERVER['DOCUMENT_ROOT'];
                $Inifile .= "/Files/Settings.ini";
                $Settings = parse_ini_file($Inifile, true);

                $Serverhostname = $Settings['Database']['Hostname'];
                $Databaseuser   = $Settings['Database']['Username'];
                $Databasepass   = $Settings['Database']['Password'];
                $Databasename   = $Settings['Database']['Database'];

                $Adminuser = $Settings['Admin']['Username'];
                $Adminpass = $Settings['Admin']['Password'];

                $Usertable = $Settings['Tables']['Usertable'];
                $Codetable = $Settings['Tables']['Codetable'];

                $Stableurl = $Settings['Loader']['Stableurl'];
                $Betaurl   = $Settings['Loader']['Betaurl'];
                $DecryptionKey = $Settings['Loader']['DecryptionKey'];

                function RandString($length = 30) {
                    $characters       = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
                    $charactersLength = strlen($characters);
                    $randomString     = '';
                    for ($i = 0; $i < $length; $i++) {
                        $randomString .= $characters[rand(0, $charactersLength - 1)];
                    }
                    return $randomString;
                }

                # Connect to Server

                $conn = new mysqli($Serverhostname, $Databaseuser, $Databasepass);

                # Check if Database exists if not create it

                $conn->query("CREATE DATABASE IF NOT EXISTS ${Databasename}");

                # Check if Table exists if not create it

                $conn = new mysqli($Serverhostname, $Databaseuser, $Databasepass, $Databasename);

                $sql = "CREATE TABLE IF NOT EXISTS ${Codetable} (
                id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                License VARCHAR(255) NOT NULL,
                Betastatus VARCHAR(1) NOT NULL,
                Expires VARCHAR(255) NOT NULL
                )";

                $conn->query($sql);

                # Calculate results

                if ($Username === $Adminuser) {
                  if ($Password === $Adminpass) {
                    for ($i = 0; $i < $Amount; $i++) {

                      $License = RandString();

                      $conn->query("INSERT INTO ${Codetable} (`License`, `Betastatus`, `Expires`) VALUES ('{$License}', '${Betastatus}','${Expires}')");

                      echo "<p>${License}</p>";
                    }
                  }
                  else {
                    echo "<p>NULL CREDENTIALS</p>";
                  }
                }
                else {
                  echo "<p>NULL CREDENTIALS</p>";
                }

                ?>

              </span>
            </div>
          </div>
        </div>

      <?php

      if(isset($_POST['Username'])) {
        if(isset($_POST['Password'])) {
          if(isset($_POST['Amount'])) {
            if(isset($_POST['Expires'])) {
              if(isset($_POST['Beta'])) {

                echo "<script> document.getElementById('id01').style.display='block' </script>";
              }
            }
          }
        }
      }

      ?>

      </form>
    </div>

    <div class="w3-display-bottommiddle w3-padding-large">
      Copyright © 2019 Hyper Hook
    </div>
</body>
</html>
