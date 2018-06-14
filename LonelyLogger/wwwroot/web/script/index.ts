var xmlhttp = new XMLHttpRequest();

xmlhttp.onreadystatechange = function () {
    if (xmlhttp.readyState == XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
        if (xmlhttp.status == 200) {
            var result = JSON.parse(xmlhttp.responseText);
            document.getElementById("myDiv").innerHTML = xmlhttp.responseText;
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