const CACHE_NAME = "sik-scoreboard-v2";

const URLS_TO_CACHE = [
    "/scoreboard/offline.html",
    "/css/site.css"
];

self.addEventListener("install", event => {
    event.waitUntil(
        caches.open(CACHE_NAME).then(cache => cache.addAll(URLS_TO_CACHE))
    );
});

self.addEventListener("activate", event => {
    event.waitUntil(self.clients.claim());
});

self.addEventListener("fetch", event => {
    event.respondWith(
        fetch(event.request).catch(() =>
            caches.match("/scoreboard/offline.html")
        )
    );
});