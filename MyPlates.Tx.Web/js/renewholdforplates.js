        var timeoutValue;
        var bodyClicked = false;
        
        StartTimer();
                 
        function StartTimer() 
        {
            // 840000 = 14 minutes
            timeoutValue = window.setInterval("ResetRenewTimer()", 840000);
        }
        
        function BodyClicked() 
        {
            bodyClicked = true;
        }
        
        function ResetRenewTimer() 
        {
            if(bodyClicked) {
                RenewPlates();
                bodyClicked = false;
            }
        }
        
        function RenewPlates() {
            var xmlhttp = null;
            
            // code for Mozilla, etc.
            if (window.XMLHttpRequest)
            {
              xmlhttp = new XMLHttpRequest();
            }
            // code for IE
            else if (window.ActiveXObject)
            {
              xmlhttp = new ActiveXObject('Microsoft.XMLHTTP');
            }
            
            if(xmlhttp != null) { 
                xmlhttp.open('GET', '/UIRenewPlateHold.aspx', true);
                xmlhttp.send(null);
                
                //window.status = 'Plate hold renewed at ' + GetCurrentTime() + '.';
            }
        }
        
        function GetCurrentTime()
        {
                var a_p = "";
                var d = new Date();

                var curr_hour = d.getHours();

                if (curr_hour < 12)
                {
                a_p = "AM";
                }
                else
                {
                a_p = "PM";
                }
                if (curr_hour == 0)
                {
                curr_hour = 12;
                }
                if (curr_hour > 12)
                {
                curr_hour = curr_hour - 12;
                }

                var curr_min = d.getMinutes();

                curr_min = curr_min + "";

                if (curr_min.length == 1)
                   {
                   curr_min = "0" + curr_min;
                   }        
                   
                return curr_hour + ':' + curr_min + ' ' + a_p                   ;
        }        