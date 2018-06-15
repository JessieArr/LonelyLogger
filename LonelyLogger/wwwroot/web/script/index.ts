
function getLogs() {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
            if (xmlhttp.status == 200) {
                var myDiv = document.getElementById("logTable");
                while (myDiv.hasChildNodes()) {
                    myDiv.removeChild(myDiv.lastChild);
                }

                var result = JSON.parse(xmlhttp.responseText);
                result.forEach(function (value) {
                    var newRow = document.createElement("tr");
                    var dateCell = document.createElement("td");
                    var messageCell = document.createElement("td");

                    var date = new Date(value.log_time);
                    dateCell.innerText = date.toLocaleString();
                    messageCell.innerText = value.message;

                    newRow.appendChild(dateCell);
                    newRow.appendChild(messageCell);

                    myDiv.appendChild(newRow);
                });
            }
            else if (xmlhttp.status == 400) {
                alert('There was an error 400');
            }
            else {
                alert('something else other than 200 was returned');
            }
        }
    };

    xmlhttp.open("GET", "/api/logs", true);
    xmlhttp.send();
}

getLogs();
setInterval(function () {
    getLogs();
}, 5000);

