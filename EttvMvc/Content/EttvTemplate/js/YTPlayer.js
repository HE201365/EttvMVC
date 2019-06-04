
var seekToTime;
var myVideoId;
var futureVideoList = [];
var remainTimeToNextVideo;
var result = [];
$.ajax({
    type: "GET",
    url: "/Home/GetIndexContentJsonResult",
    success: function (data) {
        //console.log(data)
        //console.log(data[0]["VideoContent"]["VideoId"]);
        //console.log(moment(data[0]["EndTime"]) - moment(data[0]["StartTime"]));
        for (var i = 0; i < data.length; i++) {
            result.push({
                videoId: data[i]["VideoContent"]["VideoId"],
                title: data[i]["VideoContent"]["Title"],
                duration: data[i]["VideoContent"]["Duration"],
                startTime: moment(data[i]["StartTime"]),
                endTime: moment(data[i]["EndTime"])
            });
        }
    }
});
console.log(result);

//var date = Date.now();

//function millisToMinutesAndSeconds(millis) {
//    var minutes = Math.floor(millis / 60000);
//    var seconds = ((millis % 60000) / 1000).toFixed(0);
//    return minutes + ":" + (seconds < 10 ? '0' : '') + seconds;
//}
function millisToMinutesAndSeconds(duration) {
    seconds = Math.floor((duration / 1000) % 60),
    minutes = Math.floor((duration / (1000 * 60)) % 60),
    hours = Math.floor((duration / (1000 * 60 * 60)) % 24);

    hours = (hours < 10) ? "0" + hours : hours;
    minutes = (minutes < 10) ? "0" + minutes : minutes;
    seconds = (seconds < 10) ? "0" + seconds : seconds;

    return hours + ":" + minutes + ":" + seconds;
}

function findVIdSeekTo() {
    result.forEach(findVideoAndSeekTo);
    //TODO test again
    //for (var i = 0; i < result.length; i++) {
    //    if (moment(Date.now()).isBetween(result[i]["startTime"], result[i]["endTime"])) {
    //        seekToTime = moment(Date.now()).diff(moment(result[i]["startTime"]));
    //        myVideoId = result[i]["videoId"];
    //        console.log(seekToTime, 'in findVIdSeekTo()');
    //        console.log(myVideoId, 'in findVIdSeekTo()');
    //    } else {
    //        if (moment(Date.now()).isBefore(result[i]["startTime"])) {
    //            futureVideoList[futureVideoList.length] = moment(result[i]["startTime"]).diff(moment(Date.now()));
    //        }
    //    }
    //}
    //console.log(Math.round(futureVideoList[0] / 60000));
    //remainTimeToNextVideo = futureVideoList[0];
    //console.log(remainTimeToNextVideo)
}
function findVideoAndSeekTo(item, index) {
    //console.log(index, moment(Date.now()).isBetween(item["startTime"], item["endTime"]));
    if (moment(Date.now()).isBetween(item["startTime"], item["endTime"])) {
        seekToTime = moment(Date.now()).diff(moment(item["startTime"]));
        myVideoId = item["videoId"];
        console.log(seekToTime, 'in function in if isbetween');
        console.log(myVideoId, 'in function in if isbetween');
    }
    if (myVideoId == undefined) {
        if (moment(Date.now()).isBefore(item["startTime"])) {
            futureVideoList.push(moment(item["startTime"]).diff(moment(Date.now())));
        }
    }
}
var $a = document.querySelector.bind(document); // for fullscreen

$(document).ready(function () {
    findVIdSeekTo();
    //result.forEach(findVideoAndSeekTo);
    console.log(futureVideoList);
    //console.log(millisToMinutesAndSeconds(futureVideoList[0]));
    // Remaining Time for Next Program : 
    // var ms is the variable that decrease every second 
    var ms = futureVideoList[0];
    if (myVideoId == undefined && ms > 0) {
        st = setInterval(function () { ms -= 1000; }, 1000);
        runn = setInterval(function () { $('div#remainingTime').text("Remaining Time for Next Program : " + millisToMinutesAndSeconds(ms)); }, 1000);
    }
    // refresh when time for the first next video is arrived
    var timeremainingForFirstNextVideo = futureVideoList[0];
    if (timeremainingForFirstNextVideo > 0) { setTimeout(function () { location.reload(); }, ms); }

    $(document).on('turbolinks:load', function () {
        onYouTubeIframeAPIReady();
    });

});

function progress(percent, $element) {
    var progressBarWidth = percent * $element.width() / 100;
    $element.find('div').animate({ width: progressBarWidth });
}

var tag = document.createElement('script');
tag.src = "https://www.youtube.com/player_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
var player;
//var myVideoId = 'M7lc1UVf-VE';
function onYouTubeIframeAPIReady() {
    player = new YT.Player('ytplayer', {
        height: '350',
        width: '660',
        videoId: myVideoId,
        playerVars: {
            'autoplay': 1,
            'controls': 0,
            'modestbranding': 1,
            'rel': 0,
            'theme': 'dark',
            'color': 'white',
            'cc_lang_pref': 1,
            'enablejsapi': 1,
            'origin': 'https://localhost:44328/'
        },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange,
            'onError': onPlayerError
        }
    });
}
$(document).on('turbolinks:load', function () {
    onYouTubeIframeAPIReady()
})
var iframe;
// 4. The API will call this function when the video player is ready.
function onPlayerReady(event) {
    event.target.setVolume(100);
    event.target.seekTo(seekToTime / 1000);
    event.target.playVideo();
    findVIdSeekTo();
    iframe = $a('#ytplayer');
    setupListener();
}

function onPlayerError() {
    $('.n_p_video_container')
        .html(
            '<div><img src="https://foreignpolicymag.files.wordpress.com/2018/07/gettyimages-9921744441.jpg?w=600&h=350&crop=0%2C0%2C0%2C0&quality=90"/></div>');
}
// 5. The API calls this function when the player's state changes.
//    The function indicates that when playing a video (state=1),
//    the player should play for six seconds and then stop.
function changeState(playerStatus) {
    var color;
    switch (playerStatus) {
        // case -1: // unstarted 
        case 0:  // ended 
            //if there is a gap between 2 video
            findVIdSeekTo();
            if (myVideoId !== undefined)
                location.reload();
            player.loadVideoById(myVideoId);
            break;
        // case 1:  // playing
        // case 3:     // buffering
        // case 5:     // video cued
        //case 2:     // paused 
        //    player.playVideo(player.getCurrentTime());
        //    break;
    }
}

var mytimer;
function onPlayerStateChange(event) {
    changeState(event.data);
    if (event.data == YT.PlayerState.PLAYING) {

        $('#progressBar').show();
        var playerTotalTime = player.getDuration();
        mytimer = setInterval(function () {
            var playerCurrentTime = player.getCurrentTime();
            var playerTimeDifference = (playerCurrentTime / playerTotalTime) * 100;
            //TODO if there have another video on the list play next if not stop it
            //console.log('currentTime:', Math.round(player.getCurrentTime()), 'duration:', player.getDuration());
            // if (Math.round(player.getDuration()) - Math.round(player.getCurrentTime()) < 3) { player.stopVideo(); player.videoId = 'taJ60kskkns'; player.loadVideoById(player.videoId) }
            progress(playerTimeDifference, $('#progressBar'));
        }, 1000);
    } else {
        clearTimeout(mytimer);
        $('#progressBar').hide();
    }
}

// play pause buttons
function setupListener() {
    $a('#fullscreenTop').addEventListener('click', playFullscreen);
    $a('#fullscreenBottom').addEventListener('click', playFullscreen);
    $('button#playTop').on('click', playPaly);
    $('button#pauseTop').on('click', playPause);
    $('button#playBottom').on('click', playPaly);
    $('button#pauseBottom').on('click', playPause);
}

function playFullscreen() {
    var requestFullScreen = iframe.requestFullScreen || iframe.mozRequestFullScreen || iframe.webkitRequestFullScreen;
    if (requestFullScreen) {
        requestFullScreen.bind(iframe)();
        console.log(requestFullScreen);
    }
}

function playPaly() {
    findVIdSeekTo();
    if (myVideoId !== undefined)
        location.reload();
    player.loadVideoById(myVideoId);
}
function playPause() {
    player.stopVideo();
}

//var dateTime = new Date("2015-06-17 14:24:36");
//dateTime = moment(dateTime).format("YYYY-MM-DD HH:mm:ss");

//console.log(dateTime)