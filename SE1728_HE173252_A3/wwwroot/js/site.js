// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
ShowPosts();

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalRHub").build();

connection.start().catch(err => alert(err));

connection.on("LoadPosts", function () {
    ShowPosts();
})

function ShowPosts() {
    let postList = document.getElementById("post_list");
    let currentPageElement = document.getElementById("current_page_num");
    let currentPageVal = parseInt(currentPageElement.innerText, 10);
    postList.innerHTML = "";

    fetch("/Post/Index?handler=GetPosts&pageNumber=" + currentPageVal)
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
            `
            postList.innerHTML += html;
        }))
}