<?php
    include_once '../objects/entityBase.php';

    class Exposition extends EntityBase
    {
        private $table_name = "expositions";
    
        // object properties
        public $id;
        public $name_exposition;
        public $info_exposition;
        public $hour;
        public $date;
        public $room;
        public $name_expositor;
        public $photo_expositor;
        public $info_expositor;
    
        // Obtener charlas
        function getExpositions()
        {
            // query to insert record
            $query = "SELECT * FROM  ". $this->table_name ."";

            // prepare query
            $stmt = $this->conn->prepare($query);

            // execute query
            $stmt->execute();
        
            return $stmt;
        } 
    }
?>