<?php
        // required headers
        header("Access-Control-Allow-Origin: *");
        header("Content-Type: application/json; charset=UTF-8");
        header("Access-Control-Allow-Methods: GET");
        
        // database connection will be here
        // include database and object files
        include_once '../config/database.php';
        include_once '../objects/exposition.php';
        include_once '../utilities/jsonUtility.php';

        // instantiate database and product object
        $database = new Database();
        $db = $database->getConnection();
        
        // initialize object
        $exposition = new Exposition($db);
        
        // read products will be here
        // query products
        $stmt = $exposition->getExpositions();
        $num = $stmt->rowCount();
    
        // check if more than 0 record found
        if($num>0)
        {
            // users array
            $expos_arr=array();
        
            // retrieve our table contents
            // fetch() is faster than fetchAll()
            while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
                // extract row
                // this will make $row['name'] to
                // just $name only
                extract($row);

                $expo_single=array(
                    "id" => $id,
                    "name_exposition" => $name_exposition,
                    "info_exposition" => $info_exposition,
                    "hour" => $hour,
                    "date" => $date,
                    "room" => $room,
                    "name_expositor" => $name_expositor,
                    "photo_expositor" => $photo_expositor,
                    "info_expositor" => $info_expositor
                );
        
                array_push($expos_arr, $expo_single);
            }

            // set response code - 200 OK
            http_response_code(200);

            $status = "OK";
        
            // show products data in json format
            echo json_encode(JSONUtility::GetResult($status, $expos_arr));
        }else{
            // set response code - 404 Not found
            http_response_code(404);
        
            $status = "ERROR";

            // tell the user no products found
            echo json_encode(JSONUtility::GetResult($status, "Exposition not found"));
        }
?>