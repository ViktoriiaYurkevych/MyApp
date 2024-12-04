function toggleLike(postId) {
    fetch(`/Home/ToggleLike?postId=${postId}`, {
        method: 'POST'
    }).then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload(); // Обновление страницы для отображения изменений
            }
        });
}
