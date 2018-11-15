function getLog() {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == XMLHttpRequest.DONE) {   // XMLHttpRequest.DONE == 4
            if (xmlhttp.status == 200) {
                var div = document.getElementById('logDetailDiv');

                var result = JSON.parse(xmlhttp.responseText);

                var metaDataDiv = document.createElement('div');
                var logDiv = document.createElement('div');

                var metaDataHeader = document.createElement('h3');
                metaDataHeader.innerText = "Metadata:";
                metaDataDiv.appendChild(metaDataHeader);

                var logHeader = document.createElement('h3');
                logHeader.innerText = "Log Details:";
                logDiv.appendChild(logHeader);

                for (var property in result.metaData) {
                    var tagElement = document.createElement("dt");
                    var descriptionElement = document.createElement("dd");

                    tagElement.innerText = property
                    descriptionElement.innerText = result.metaData[property]

                    metaDataDiv.appendChild(tagElement);
                    metaDataDiv.appendChild(descriptionElement);
                }

                for (var property in result.log) {
                    var tagElement = document.createElement("dt");
                    var descriptionElement = document.createElement("dd");

                    tagElement.innerText = property
                    descriptionElement.innerText = result.log[property]

                    logDiv.appendChild(tagElement);
                    logDiv.appendChild(descriptionElement);
                }

                div.appendChild(metaDataDiv);
                div.appendChild(logDiv);
            }
            else if (xmlhttp.status == 400) {
                console.log('There was an error 400');
            }
            else {
                console.log('something else other than 200 was returned');
            }
        }
    };

    const urlParams = new URLSearchParams(window.location.search);
    const id = urlParams.get('id');

    xmlhttp.open("GET", "/api/log?id=" + id, true);
    xmlhttp.send();
}

getLog();