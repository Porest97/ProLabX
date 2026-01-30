let homeScore = 0;
let awayScore = 0;
let period = 1;

let totalSeconds = 20 * 60;
let interval = null;

// ===== SCORE =====
function changeScore(team, delta) {
    if (team === "home") {
        homeScore = Math.max(0, homeScore + delta);
        document.getElementById("homeScore").innerText = homeScore;
    } else {
        awayScore = Math.max(0, awayScore + delta);
        document.getElementById("awayScore").innerText = awayScore;
    }
}

// ===== PERIOD =====
function changePeriod(delta) {
    period += delta;
    if (period < 1) period = 1;
    if (period > 6) period = 6;
    document.getElementById("period").innerText = period;
}

// ===== CLOCK =====
function renderClock() {
    const m = String(Math.floor(totalSeconds / 60)).padStart(2, "0");
    const s = String(totalSeconds % 60).padStart(2, "0");
    document.getElementById("clock").innerText = `${m}:${s}`;
}

function startClock() {
    if (interval) return;
    interval = setInterval(() => {
        if (totalSeconds > 0) {
            totalSeconds--;
            renderClock();
        }
    }, 1000);
}

function stopClock() {
    clearInterval(interval);
    interval = null;
}

function resetClock() {
    stopClock();
    totalSeconds = 20 * 60;
    renderClock();
}

renderClock();