<?php
    // required headers
    header("Access-Control-Allow-Origin: *");
    header("Content-Type: application/json; charset=UTF-8");
    header("Access-Control-Allow-Methods: POST");
    header("Access-Control-Max-Age: 3600");
    header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

    // get database connection
    include_once '../config/database.php';    
    include_once '../objects/user.php';
    include_once '../utilities/jsonUtility.php';
        
    $database = new Database();
    $db = $database->getConnection();
        
    $user = new User($db);
    
    $status = "ERROR";

    // get posted data
    $user_id = isset($_POST["user_id"]) ? $_POST["user_id"] : "";
    $expo_id = isset($_POST["expo_id"]) ? $_POST["expo_id"] : "";
    
    // Determinar si los datos son enviados
    if(!empty($user_id) &&
        !empty($expo_id))
    {
        // set ID property of user to be edited
        $user->id = $user_id;

        $stmt = $user->getUserById();
        $num = $stmt->rowCount();

        // register the user
        if($num > 0)
        {   
            // update the user
            if($user->deleteLike($expo_id)){
            
                $status = "OK";
                
                // set response code - 200 ok
                http_response_code(200);
            
                // tell the user
                echo json_encode(JSONUtility::GetResult($status, "user was updated."));
            }
            
            // if unable to update the user, tell the user
            else{
            
                // set response code - 503 service unavailable
                http_response_code(503);
            
                // tell the user
                echo json_encode(JSONUtility::GetResult($status, "Unable to update user."));
            }
        }else{
            // set response code - 400 bad request
            http_response_code(400);
        
            // tell the user
            echo json_encode(JSONUtility::GetResult($status, "User doesnt exist."));
        }
    }// tell the user data is incomplete
    else{
    
        // set response code - 400 bad request
        http_response_code(400);
    
        // tell the user
        echo json_encode(JSONUtility::GetResult($status, "Unable to update user. Data is incomplete."));
    }
?>