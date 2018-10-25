var lonelyLogger = function (newHostName) {
    if (!newHostName) {
        throw "LonelyLogger hostname not provided!";
    }
    return {
        hostname: newHostName,
        writeLog: function (log) {
            var http = new XMLHttpRequest();
            var url = "http://" + this.hostname + "/api/logs";
            http.open("POST", url, true);

            http.onreadystatechange = function () {
                if (http.readyState == 4 && http.status == 200) {
                    console.log(http.responseText);
                }
            };
            http.setRequestHeader("Accept", "application/json");
            http.setRequestHeader("Content-Type", "application/json; charset=UTF-8");

            var logString = JSON.stringify(log);
            http.send(logString);
        }
    }
}