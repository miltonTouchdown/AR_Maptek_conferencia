<?php
class EntityBase{
 
    // database connection and table name
    protected $conn;

    /**
     * constructor with $db as database connection
     *
     * //@param Place   $where  Where something interesting takes place
     * //@param integer $repeat How many times something interesting should happen
     * 
     * @throws Some_Exception_Class If something interesting cannot happen
     * @author Milton Pulgar
     * @return Status
    */ 
    public function __construct($db){
        $this->conn = $db;
    }

    function create(){}

    function update(){}

    function delete(){}

    function sanitizeValues(){}

    function bindSQLValues($stmt){}
}