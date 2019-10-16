<?php

# Get variables from post method

$Username = $_POST['Username'];
$License  = $_POST['License'];
$HWID     = $_POST['HWID'];

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

# Connect to Server

$conn = new mysqli($Serverhostname, $Databaseuser, $Databasepass);

# Check if Database exists if not create it

$conn->query("CREATE DATABASE IF NOT EXISTS ${Databasename}");

# Check if Table exists if not create it

$conn = new mysqli($Serverhostname, $Databaseuser, $Databasepass, $Databasename);

$sql = "CREATE TABLE IF NOT EXISTS ${Usertable} (
id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Username VARCHAR(255) NOT NULL,
Password VARCHAR(255) NOT NULL,
Betastatus VARCHAR(1) NOT NULL,
HWID VARCHAR(255),
Expires VARCHAR(255),
Updated TIMESTAMP
)";

$conn->query($sql);

$sql = "CREATE TABLE IF NOT EXISTS ${Codetable} (
id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
License VARCHAR(255) NOT NULL,
Betastatus VARCHAR(1) NOT NULL,
Expires VARCHAR(255) NOT NULL
)";

$conn->query($sql);

# Calculate results

$sql = "SELECT * FROM ${Usertable} WHERE Username = '${Username}'";

if ($conn->query($sql)->num_rows == 1) {
  $Currentsub = $conn->query($sql)->fetch_object()->Expires;

  $sql = "SELECT * FROM ${Codetable} WHERE License = '${License}'";

  if ($conn->query($sql)->num_rows == 1) {
    $Newsub = $conn->query($sql)->fetch_object()->Expires;
    $Betastatus = $conn->query($sql)->fetch_object()->Betastatus;

    $conn->query("DELETE FROM `${Databasename}`.`${Codetable}` WHERE `License`='${License}'");
    $conn->query("UPDATE `${Databasename}`.`${Usertable}` SET `Betastatus`='${Betastatus}', `Expires`='${Newsub}' WHERE `Username`='${Username}'");

    if ($Newsub >= 25000000) {
      echo "{'Username':'${Username}','Authenticated':'true','Description':'Account ${Username} successfully updated (Expires in: Never).'}";
    }
    else {
      echo "{'Username':'${Username}','Authenticated':'true','Description':'Account ${Username} successfully updated (Expires in: ${Newsub} days).'}";
    }

    die();
  }

  echo "{'Username':'${Username}','Authenticated':'false','Description':'Sorry ${Username} account failed to update.'}";
}
else {
  if ($conn->query($sql)->num_rows > 1) {
    echo "{'Username':'${Username}','Authenticated':'false','Description':'Sorry ${Username} account failed to update.'}";
  }

  echo "{'Username':'${Username}','Authenticated':'false','Description':'Sorry ${Username} an account with these details does not exist.'}";
}

 ?>
