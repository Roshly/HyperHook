<?php

# Get variables from post method

$Username = $_GET['Username'];
$Password = $_GET['Password'];

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

# Calculate results

if ($Username === $Adminuser) {
  if ($Password === $Adminpass) {
    $conn->query("UPDATE `${Databasename}`.`${Usertable}` SET Expires = Expires - 1 WHERE Expires > 0 and Expires < 25000000");
  }
}

# Close our connection

$conn->close();

?>
