<?php
    include_once '../objects/entityBase.php';

    class User extends EntityBase
    {
        private $table_name = "users";
    
        // object properties
        public $id;
        public $email;
        public $library;
    
        function getUserByMail()
        {
            // select all query
            $query = "SELECT 
                    id,
                    email
            FROM
            ".$this->table_name." 
            WHERE users.email = ? LIMIT 0,1";

            // prepare query statement
            $stmt = $this->conn->prepare($query);
        
            // bind id of product to be updated
            $stmt->bindParam(1, $this->email);

            // execute query
            $stmt->execute();

            return $stmt;
        }

        function getUserById()
        {
            // select all query
            $query = "SELECT 
                    email
            FROM
            ". $this->table_name." WHERE users.id = ? LIMIT 0,1";

            // prepare query statement
            $stmt = $this->conn->prepare($query);
        
            // bind id of product to be updated
            $stmt->bindParam(1, $this->id);

            // execute query
            $stmt->execute();

            return $stmt;
        }

        // create user
        function register()
        {
            // query to insert record
            $query = "INSERT INTO
                        " . $this->table_name . "
                    SET
                        email=:email";
        
            // prepare query
            $stmt = $this->conn->prepare($query);
        
            // sanitize
            $this->email=htmlspecialchars(strip_tags($this->email));
                   
            // bind values
            $stmt->bindParam(":email", $this->email);
        
            // execute query
            if($stmt->execute()){
                return $stmt;
            }
        
            return false;          
        }

        // Obtener likes
        function getLibraryById()
        {
            $table_name_library = "library_user_likes";

            // query to insert record
            $query = "SELECT * FROM  ". $table_name_library ." 
                WHERE users_id = ?";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // bind id of product to be updated
            $stmt->bindParam(1, $this->id);

            // execute query
            $stmt->execute();
        
            return $stmt;
        }

        function addLike($expo_id)
        {
            $table_name_library = "library_user_likes";

            $query ="INSERT INTO
                    ".$table_name_library."
                SET
                    users_id = :user_id,
                    expositions_id = :expo_id";

            // prepare query
            $stmt = $this->conn->prepare($query);
        
            // sanitize
            $this->id=htmlspecialchars(strip_tags($this->id));
            $expo_id=htmlspecialchars(strip_tags($expo_id));
                   
            // bind values
            $stmt->bindParam(":user_id", $this->id);
            $stmt->bindParam(":expo_id", $expo_id);
        
            // execute query
            if($stmt->execute()){
                return $stmt;
            }
        
            return false;     
        }

        function deleteLike($expo_id)
        {
            $table_name_library = "library_user_likes";

            $query ="DELETE FROM
                    ".$table_name_library."
                WHERE
                    users_id = :user_id AND
                    expositions_id = :expo_id";

            // prepare query
            $stmt = $this->conn->prepare($query);
        
            // sanitize
            $this->id=htmlspecialchars(strip_tags($this->id));
            $expo_id=htmlspecialchars(strip_tags($expo_id));
                   
            // bind values
            $stmt->bindParam(":user_id", $this->id);
            $stmt->bindParam(":expo_id", $expo_id);
        
            // execute query
            if($stmt->execute()){
                return $stmt;
            }
        
            return false;     
        }   
    }
?>