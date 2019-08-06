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
    
    // get posted data
    $email = isset($_POST["email"]) ? $_POST["email"] : "";

    $status = "ERROR";
    
    // make sure data is not empty
    if(
        !empty($email)
    ){
    
        // set user property values
        $user->email = $email;

        // Evitar crear usuario ya existente
        $stmt = $user->getUserByMail();
        $num = $stmt->rowCount();

        if($num == 0)
        {
            // register the user
            if($user->register()){
                
                $status = "OK";
       
                $stmt = $user->getUserByMail();

                $row = $stmt->fetch(PDO::FETCH_ASSOC);
           
                $user->id = $row['id'];

                $like_user=array();
            
                $stmt = $user->getLibraryById();
                // retrieve our table contents
                // fetch() is faster than fetchAll()
                while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
                    // extract row
                    // this will make $row['name'] to
                    // just $name only
                    extract($row);

                    $id_single=array(
                        "id" => $Expositions_id
                    );
            
                    array_push($like_user, $id_single);
                }

                $user->library = $like_user;

                // set response code - 201 created
                http_response_code(201);

                echo json_encode(JSONUtility::GetResult($status, $user));
            }
        
            // if unable to create the user, tell the user
            else{
        
                // set response code - 503 service unavailable
                http_response_code(503);
        
                // tell the user
                echo json_encode(JSONUtility::GetResult($status, "Unable to create user."));
            }
        }else{
            http_response_code(400);
            // tell the user
            echo json_encode(JSONUtility::GetResult($status, "user already exist."));           
        }
    } 
    // tell the user data is incomplete
    else{
    
        // set response code - 400 bad request
        http_response_code(400);
    
        // tell the user
        echo json_encode(JSONUtility::GetResult($status, "Unable to create user. Data is incomplete."));
    }
?>