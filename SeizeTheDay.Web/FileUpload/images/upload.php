<?php 
      $ds = DIRECTORY_SEPARATOR;

      $storeFolder = 'images3';

      if (!empty($_FILES)) 
      {
             $tempFile = $_FILES['file']['tmp_name'];

             $targetPath = dirname( __FILE__ ) . $ds. $storeFolder . $ds;

             $file_name = substr(md5(rand(1, 213213212)), 1, 5) . "_" . str_replace(array('\'', '"', ' ', '`'), '_', $_FILES['file']['name']);

             $targetFile =  $targetPath. $file_name;

             if(move_uploaded_file($tempFile,$targetFile)){
                   die( $_SERVER['HTTP_REFERER']. $storeFolder . "/" . $file_name );
              }else{
                   die('Fail');
              }

       }
?>