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

$Stableurl = $Settings['Loader']['Stableurl'];
$Betaurl   = $Settings['Loader']['Betaurl'];
$DecryptionKey = $Settings['Loader']['DecryptionKey'];

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

$sql = "SELECT * FROM ${Usertable} WHERE Username = '${Username}' and Password = '${Password}'";

if ($conn->query($sql)->num_rows === 1) {

    $Hwidvalue  = $conn->query($sql)->fetch_object()->HWID;
    $Betastatus = $conn->query($sql)->fetch_object()->Betastatus;

    if ($Hwidvalue === 0) {
        $conn->query("UPDATE `${databasename}`.`${Usertable}` SET `HWID`='${HWID}' WHERE Username = '${Username}' and Password = '${Password}'");
    }

    if ($conn->query("SELECT * FROM ${Usertable} WHERE Username = '${Username}' and Password = '${Password}' and HWID = '${HWID}'")->num_rows === 1) {
        $Timeleft = $conn->query($sql)->fetch_object()->Expires;
        $Betastatus = $conn->query($sql)->fetch_object()->Betastatus;

        if ($Timeleft > 0) {
          if ($Timeleft >= 25000000) {
              echo "{'Username':'${Username}','Expires':'Never','Authenticated':'true','Betastatus':'${Betastatus}','stableurl':'${Stableurl}','betaurl':'${Betaurl}','DecryptionKey':'${DecryptionKey}','Description':'Welcome ${Username} you have successfully logged in.'}";
          }
          else {
              echo "{'Username':'${Username}','Expires':'${Timeleft}','Authenticated':'true','Betastatus':'${Betastatus}','stableurl':'${Stableurl}','betaurl':'${Betaurl}','DecryptionKey':'${DecryptionKey}','Description':'Welcome ${Username} you have successfully logged in.'}";
          }
        }
        else {
          echo "{'Username':'${Username}','Expires':'${Timeleft}','Authenticated':'false','Description':'Sorry ${Username} your subscription has expired.'}";
        }

        die();
    }
}

if ($conn->query($sql)->num_rows >= 1) {
  echo "{'Username':'${Username}','Expires':'0','Authenticated':'false','Description':'Sorry ${Username} an unknown error has occured.'}";
}
else {
  echo "{'Username':'${Username}','Expires':'0','Authenticated':'false','Description':'Sorry ${Username} an account with these details does not exist.'}";
}

# Close our connection

$conn->close();

?>
