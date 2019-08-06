<?php
    // required headers
    header("Access-Control-Allow-Origin: *");
    header("Access-Control-Allow-Headers: access");
    header("Access-Control-Allow-Methods: GET");
    header("Access-Control-Allow-Credentials: true");
    header('Content-Type: application/json');
    
    // get database connection
    include_once '../config/database.php';    
    include_once '../objects/user.php';
    include_once '../utilities/jsonUtility.php';
    
    $database = new Database();
    $db = $database->getConnection();
    
    $user = new User($db);

    $status = "ERROR";

    // get keywords
    $email = isset($_GET["email"]) ? $_GET["email"] : "";

    // make sure data is not empty
    if(
        !empty($email)
    ){
    
        // set user property values
        $user->email = $email;
            
        //password_hash($password, PASSWORD_DEFAULT); 
        //password_verify($password, $hashed_password)
        $stmt = $user->getUserByMail();
        $num = $stmt->rowCount();

        // register the user
        if($num == 1)
        {  
            $status = "OK";

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
                    "id" => $expositions_id
                );
            
                array_push($like_user, $id_single);
            }

            $user->library = $like_user;

            // set response code - 201 created
            http_response_code(201);

            echo json_encode(JSONUtility::GetResult($status, $user));
        }   
        // if unable to create the user, tell the user
        else
        {
            // set response code - 503 service unavailable
            http_response_code(400);
    
            // tell the user
            echo json_encode(JSONUtility::GetResult($status, "User incorrect."));
        }
    }
    
    // tell the user data is incomplete
    else{
    
        // set response code - 400 bad request
        http_response_code(400);
    
        // tell the user
        echo json_encode(JSONUtility::GetResult($status, "Unable to login user. Data is incomplete."));
    }
?>