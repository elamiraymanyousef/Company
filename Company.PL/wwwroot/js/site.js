// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let searchValue = document.getElementById('searchValue');
searchValue.addEventListener('keyup', () => {

    let xhr = new XMLHttpRequest();

    let searchValue = document.getElementById('searchValue');

    let url = $`https://localhost:7252/Employee{searchValue}`;

    xhr.open('GET', url, true);

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200)
        {
            console.log(JSON.parse(xhr.responseText)); // Handle response
        }
    };
    xhr.send();


});
