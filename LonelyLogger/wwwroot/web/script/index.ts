
function getLogs() {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
            if (xmlhttp.status == 200) {
                var myDiv = document.getElementById("logTableBody");
                while (myDiv.hasChildNodes()) {
                    myDiv.removeChild(myDiv.lastChild);
                }

                var result = JSON.parse(xmlhttp.responseText);
                result.forEach(function (val) {
                    var newRow = document.createElement("tr");
                    var anchorElement = document.createElement("a");
                    var dateCell = document.createElement("td");
                    var messageCell = document.createElement("td");

                    anchorElement.href = "./details.html?id=" + val.metaData.logId;
                    var date = new Date(val.metaData.receivedTime);
                    anchorElement.innerText = date.toLocaleString();

                    dateCell.appendChild(anchorElement);

                    messageCell.innerText = val.log.message;

                    newRow.appendChild(dateCell);
                    newRow.appendChild(messageCell);

                    myDiv.appendChild(newRow);
                });
            }
            else if (xmlhttp.status == 400) {
                console.log('There was an error 400');
            }
            else {
                console.log('something else other than 200 was returned');
            }
        }
    };

    xmlhttp.open("GET", "/api/logs", true);
    xmlhttp.send();
}

function getServerStatus() {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
            if (xmlhttp.status == 200) {
                var available = document.getElementById("availableSpace");
                var total = document.getElementById("totalSpace");
                var startupTime = document.getElementById("startupTime");
                var uptime = document.getElementById("uptime");
                var ramUsage = document.getElementById("ramUsage");
                
                var result = JSON.parse(xmlhttp.responseText);      
                available.innerText = result.availableSpace;
                total.innerText = result.totalSpace;
                var startupTimeDate = new Date(result.startupTime);
                startupTime.innerText = startupTimeDate.toLocaleString();
                uptime.innerText = result.uptime;
                ramUsage.innerText = result.currentRAMUsage;
            }
            else if (xmlhttp.status == 400) {
                console.log('There was an error 400');
            }
            else {
                console.log('something else other than 200 was returned');
            }
        }
    };

    xmlhttp.open("GET", "/webui", true);
    xmlhttp.send();
}

getLogs();
getServerStatus();
setInterval(function () {
    getLogs();
}, 5000);

