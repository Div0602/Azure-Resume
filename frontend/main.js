window.addEventListener('DOMContentLoaded', (event) => {
    getVisitCount();
});

const functionAPI = 'http://localhost:7071/GetResumeCounter';

const getVisitCount = () => {
    let count = 20;
    fetch(functionAPI).then(response => {
        return response.json();
    }).then(response => {
        console.log("Function API is called by the site", response);
        count = response.count;
        document.getElementById("counter").innerText = count;
    }).catch(function(error) {
        console.error(error);
    });
    return count;
}