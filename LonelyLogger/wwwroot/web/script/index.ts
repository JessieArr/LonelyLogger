var xmlhttp = new XMLHttpRequest();

xmlhttp.onreadystatechange = function () {
    if (xmlhttp.readyState == XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
        if (xmlhttp.status == 200) {
            var myDiv = document.getElementById("myDiv");

            var result = JSON.parse(xmlhttp.responseText);
            result.forEach(function (value) {
                var newParagraph = document.createElement("p");
                newParagraph.innerText = value.message;
                myDiv.appendChild(newParagraph);
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