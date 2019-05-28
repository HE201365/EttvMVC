
var seekToTime;
var myVideoId;
var result = [];
$.ajax({
    type: "GET",
    url: "/Home/GetIndexContentJsonResult",
    success: function (data) {
        console.log(data[0]["VideoContent"]["VideoId"]);
        console.log(moment(data[0]["EndTime"]) - moment(data[0]["StartTime"]));
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

var date = Date.now();
$(document).ready(function () { result.forEach(findVideoAndSeekTo);  });
function findVideoAndSeekTo(item, index) {
    console.log(index, moment(Date.now()).isBetween(item["startTime"], item["endTime"]));
    if (moment(Date.now()).isBetween(item["startTime"], item["endTime"])) {
        seekToTime = moment(Date.now()).diff(moment(item["startTime"]));
        myVideoId = item["videoId"];
        console.log(seekToTime, 'in function');
        console.log(myVideoId, 'in function');
    }
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
    //    }
    //}
}

//TODO function with for loop ?

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
            'onStateChange': onPlayerStateChange
        }
    });
}
//var stringList = '9sWEecNUW-o,taJ60kskkns';

// 4. The API will call this function when the video player is ready.
function onPlayerReady(event) {
    event.target.setVolume(100);
    event.target.seekTo(seekToTime/1000);
    event.target.playVideo();
}

// 5. The API calls this function when the player's state changes.
//    The function indicates that when playing a video (state=1),
//    the player should play for six seconds and then stop.
function changeState(playerStatus) {
    var color;
    switch (playerStatus) {
        // case -1: // unstarted 
        case 0:  // ended 
            //player.videoId = 'taJ60kskkns';
            findVIdSeekTo();
            player.loadVideoById(myVideoId);
            break;
        // case 1:  // playing
        // case 3:     // buffering
        // case 5:     // video cued
        case 2:     // paused 
            player.playVideo(player.getCurrentTime());
            break;
    }
}
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
var dateTime = new Date("2015-06-17 14:24:36");
dateTime = moment(dateTime).format("YYYY-MM-DD HH:mm:ss");

console.log(dateTime)