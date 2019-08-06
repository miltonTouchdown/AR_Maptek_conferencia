<?php
    class JSONUtility
    {
        private function __construct() {}
            
        public static function GetResult($status, $message)
        {
            $JSONRequest = array();
            $JSONRequest["status"] = $status;
            $JSONRequest["message"] = $message;

            //array_push($JSONRequest["message"], $message);

            return $JSONRequest;
        }
    }
?>