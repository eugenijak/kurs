//function myMap() {
//    var mapProp = {
//        center: new google.maps.LatLng(55.0944, 61.2411),
//        zoom: 7,
//    };
//    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

//    var components = [];

//    for (i in components) {
//        components[i].setMap(null);
//    }

//    var imageStart = "../../images/start.png";

//    var imageStop = "../../images/stop.png";

//    $.post("/Home/FileGPX")
//        .done(function (gpx) {
//            if (gpx != undefined) {

//                var points = [];
//                var arr_lat = [];
//                var arr_lon = [];
//                var bounds = new google.maps.LatLngBounds();

//                var xml = $.parseXML(gpx),
//                    $xml = $(xml);

//                $xml.find("trkpt").each(function () {
//                    var lat = $(this).attr("lat");
//                    var lon = $(this).attr("lon");
//                    arr_lat.push(lat);
//                    arr_lon.push(lon);
//                    var p = new google.maps.LatLng(lat, lon);
//                    points.push(p);
//                    bounds.extend(p);
//                })

//                var poly = new google.maps.Polyline({
//                    path: points,
//                    strokeColor: "#FF0000",
//                    strokeOpacity: .7,
//                    strokeWeight: 4
//                });

//                components.push(poly);

                    
//                var startMarker = new google.maps.Marker({
//                    position: new google.maps.LatLng(arr_lat[0], arr_lon[0]),
//                    map: map,
//                    icon: imageStart
//                });

//                components.push(startMarker);

                    
//                var stopMarker = new google.maps.Marker({
//                    position: new google.maps.LatLng(arr_lat[arr_lat.length - 1], arr_lon[arr_lon.length - 1]),
//                    map: map,
//                    icon: imageStop
//                });

//                components.push(stopMarker);

//                poly.setMap(map);

//                map.fitBounds(bounds);
                
//            }

//        })
//}