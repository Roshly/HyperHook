<?php

# Get variables from post method

$Username = $_POST['Username'];
$Password = $_POST['Password'];
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

# Calculate results

if ($conn->query("SELECT * FROM ${Usertable} WHERE Username = '${Username}'")->num_rows >= 1) {
    echo "{'Username':'${Username}','Authenticated':'false','Description':'Sorry account ${Username} already exists.'}";
}
else {
    $conn->query("INSERT INTO ${Usertable} (Username, Password, Betastatus, HWID, Expires) VALUES ('${Username}', '${Password}', '0', '${HWID}', '0')");

    echo "{'Username':'${Username}','Authenticated':'true','Description':'Account ${Username} successfully created.'}";
}

# Close our connection

$conn->close();

?>
