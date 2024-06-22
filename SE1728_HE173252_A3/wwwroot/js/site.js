// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalRHub").build();

connection.start().catch(err => alert(err));

connection.on("LoadPosts", function () {
    ShowPosts();
});

connection.on("LoadMyPosts", function () {
    console.log("Called LoadMyPosts");
    ShowMyPosts();
});


function ShowPosts() {
    let postList = document.getElementById("post_list");
    let currentPageElement = document.getElementById("current_page_num");
    let currentPageVal = parseInt(currentPageElement.innerText, 10);
    postList.innerHTML = "";

    fetch("/Post/Index?handler=GetPosts&pageNumber=" + currentPageVal)
        .then(res => res.json())
        .then(data => data.forEach(item => {
            let html = `
            <div class="card mt-5">
                <div class="card-header" style="font-weight: bold; font-size: 24px">
                    ${item.Author} posted in the ${item.Category} category
                </div>
                <div class="card-body">
                    <h3 class="card-title">${item.Title}</h3>
                    <hr />
                    <p class="card-text">
                        ${item.Content}
                    </p>
                </div>
                <div class="card-footer text-muted">
                    Posted on ${item.CreatedDate} <span style="font-style:italic">(${item.UpdatedDate})</span>
                </div>
             </div>
            `
            postList.innerHTML += html;
        }));
    LoadTotalPages();
};

function LoadButton() {
    const currentPageElement = document.getElementById('current_page_num');
    const btnPrev = document.querySelector('.btnPrev');
    const btnNext = document.querySelector('.btnNext');
    var totalPagesElement = document.getElementById("maxPage");

    let currentPage = parseInt(currentPageElement.innerText, 10);

    let prevPage = currentPage - 1;
    let nextPage = currentPage + 1;

    btnPrev.setAttribute('data-id', prevPage);
    btnNext.setAttribute('data-id', nextPage);

    if (prevPage < 1) {
        btnPrev.classList.add('disabled');
    } else {
        btnPrev.classList.remove('disabled');
    }

    if (currentPage == totalPagesElement.value) {
        btnNext.classList.add('disabled');
    } else {
        btnNext.classList.remove('disabled');
    }
};

function LoadTotalPages() {
    let totalPagesElement = document.getElementById("maxPage");
    fetch("/Post/Index?handler=GetTotalPages")
        .then(res => res.text())
        .then(data => {
            console.log("Total pages:" + data);
            totalPagesElement.value = data;
            LoadButton();
        })
        .catch(error => {
            console.error('Error fetching the total pages: ', error);
        });
};

function MakeQuery(page) {
    let postList = document.getElementById("post_list");
    postList.innerHTML = "";
    fetch("/Post/Index?handler=GetPosts&pageNumber=" + page)
        .then(res => res.json())
        .then(data => data.forEach(item => {
            console.log(data);
            let html = `
                                                    <div class="card mt-5">
                                                        <div class="card-header" style="font-weight: bold; font-size: 24px">
                                                            ${item.Author} posted in the ${item.Category} category
                                                        </div>
                                                        <div class="card-body">
                                                            <h3 class="card-title">${item.Title}</h3>
                                                            <hr />
                                                            <p class="card-text">
                                                                ${item.Content}
                                                            </p>
                                                        </div>
                                                        <div class="card-footer text-muted">
                                                            Posted on ${item.CreatedDate}
                                                        </div>
                                                     </div>
                                                    `;
            postList.innerHTML += html;
        }));
}

function ShowMyPosts() {
    let tbody = document.getElementById("my_post_table");
    tbody.innerHTML = "";

    fetch("/Post/MyPosts?handler=GetMyPosts")
        .then(res => res.json())
        .then(data => data.forEach(item => {
            let html = `<tr>
                <td>${item.PostID}</td>
                <td style="max-width:250px">${item.Title}</td>
                <td>${item.Category}</td>
                <td>${item.CreatedDate}</td>
                <td>${item.UpdatedDate}</td>
                <td>
                    <a href="/Post/Edit?id=${item.PostID}" class="btn btn-warning">Edit</a> |
                    <a href="/Post/Details?id=${item.PostID}" class="btn btn-success">Details</a> |
                    <a class="btn btn-danger btnDelete" data-id="${item.PostID}">Delete</a>
                </td>
            </tr>
            `;
            tbody.innerHTML += html;
        }));
};