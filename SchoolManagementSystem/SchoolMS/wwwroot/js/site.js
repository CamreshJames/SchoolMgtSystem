document.addEventListener('DOMContentLoaded', function () {
    const content = document.getElementById('content');

    // Function to handle page transitions
    function navigateTo(url) {
        // Fade out current page
        anime({
            targets: content,
            opacity: 0,
            duration: 500,
            easing: 'easeOutQuad',
            complete: function () {
                // Fetch and load the new page
                fetch(url)
                    .then(function (response) {
                        return response.text();
                    })
                    .then(function (html) {
                        // Create a temporary element to hold the new page content
                        const tempDiv = document.createElement('div');
                        tempDiv.innerHTML = html;
                        const newContent = tempDiv.querySelector('#content').innerHTML;

                        // Update the content with the new page content
                        content.innerHTML = newContent;

                        // Fade in the new page
                        anime({
                            targets: content,
                            opacity: 1,
                            duration: 500,
                            easing: 'easeInQuad'
                        });
                    });
            }
        });
    }

    // Example usage: Replace the URL with the target page URL
    navigateTo('target-page.html');
});
document.addEventListener('DOMContentLoaded', function () {
    anime.timeline({
        targets: '.card',
        opacity: [0, 1],
        duration: 1000,
        easing: 'easeInOutQuad'
    }).add({
        targets: '.btn',
        opacity: [0, 1],
        translateY: [-20, 0],
        delay: anime.stagger(200),
        easing: 'easeInOutQuad'
    });

    const buttons = document.querySelectorAll('.btn');

    buttons.forEach(function (button) {
        button.addEventListener('mouseenter', function () {
            anime({
                targets: button,
                scale: 1.1,
                duration: 200,
                easing: 'easeInOutQuad'
            });

            // Create multiple bubble elements
            const numBubbles = 10;
            for (let i = 0; i < numBubbles; i++) {
                const bubble = document.createElement('span');
                bubble.classList.add('bubble');
                button.appendChild(bubble);

                // Animate the bubbles
                anime({
                    targets: bubble,
                    translateX: anime.random(-100, 100) + 'px',
                    translateY: anime.random(-100, 100) + 'px',
                    scale: [0, anime.random(0.5, 1.5)],
                    opacity: [1, 0],
                    duration: anime.random(500, 1000),
                    easing: 'easeOutExpo',
                    delay: anime.random(0, 300),
                    complete: function () {
                        button.removeChild(bubble); // Remove the bubble element after animation
                    }
                });
            }
        });

        button.addEventListener('mouseleave', function () {
            anime({
                targets: button,
                scale: 1,
                duration: 200,
                easing: 'easeInOutQuad'
            });
        });
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const zoomElement = document.querySelector('.zoom-element');
    const zoomDuration = 500;
    const maxZoomLevel = 1.2;

    document.addEventListener('mousemove', function (event) {
        const { clientX, clientY } = event;
        const { left, top, width, height } = zoomElement.getBoundingClientRect();
        const centerX = left + width / 2;
        const centerY = top + height / 2;
        const deltaX = clientX - centerX;
        const deltaY = clientY - centerY;
        const distance = Math.sqrt(deltaX * deltaX + deltaY * deltaY);
        const zoomLevel = 1 + (maxZoomLevel - 1) * (distance / (width + height));

        anime({
            targets: zoomElement,
            scale: zoomLevel,
            duration: zoomDuration,
            easing: 'easeOutQuad'
        });
    });
});
function toggleSidebar() {
    var sidebar = document.getElementById("sidebar");
    sidebar.classList.toggle("sidebar-collapsed");
}