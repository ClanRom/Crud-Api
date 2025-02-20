const postForm = document.getElementById('postForm');
const postList = document.getElementById('postList');
const apiUrl = '/movies';

function fetchPosts() {
    const xhr = new XMLHttpRequest();
    xhr.open('GET', apiUrl);
    xhr.onload = function () {
        if (xhr.status === 200) {
            const posts = JSON.parse(xhr.responseText);
            postList.innerHTML = '';
            posts.forEach(post => {
                const li = document.createElement('li');
                li.innerHTML = `
                    <h3>${post.title}</h3>
                    <p>${post.description}</p>
                    <button class="delete" onclick="deletePost(${post.id})">Удалить</button>
                `;
                postList.appendChild(li);
            });
        } else {
            console.error('Error:', xhr.statusText);
        }
    };
    xhr.send();
}

postForm.addEventListener('submit', function (e) {
    e.preventDefault();
    const title = document.getElementById('title').value;
    const description = document.getElementById('description').value;

    const xhr = new XMLHttpRequest();
    xhr.open('POST', apiUrl);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function () {
        if (xhr.status === 201) {
            postForm.reset();
            fetchPosts();
        }
    };
    xhr.send(JSON.stringify({ title, description }));
});

function deletePost(id) {
    const xhr = new XMLHttpRequest();
    xhr.open('DELETE', `${apiUrl}/${id}`);
    xhr.onload = function () {
        if (xhr.status === 204) {
            fetchPosts();
        }
    };
    xhr.send();
}

fetchPosts();