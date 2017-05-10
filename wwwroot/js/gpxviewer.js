function myMap() {
    $('#errorfile').hide();
    var mapProp = {
        center: new google.maps.LatLng(55.0944, 61.2411),
        zoom: 7,
    };
    var map = new google.maps.Map(document.getElementById('googleMap'), mapProp);

    var components = [];

    $(".gpx-file input").change(function () {
        var filename = $(this).val().replace(/.*\\/, "");
        $("#filename").val(filename);
        $('#errorfile').hide();
    });

    $('#btn-file').click(
        function () {
            
            var file = document.getElementById("file").files[0];
            var reader = new FileReader();

            reader.onload = function (e) {
                xmlDoc = $.parseXML(e.target.result),
                    $xml = $(xmlDoc);

                var points = [];
                var arr_lat = [];
                var arr_lon = [];
                var bounds = new google.maps.LatLngBounds();

                $xml.find("trkpt").each(function () {
                    var lat = $(this).attr("lat");
                    var lon = $(this).attr("lon");
                    arr_lat.push(lat);
                    arr_lon.push(lon);
                    var p = new google.maps.LatLng(lat, lon);
                    points.push(p);
                    bounds.extend(p);
                })

                if (points.length === 0) {
                    $('#errorfile').show();
                }

                else {
                    for (i in components) {
                        components[i].setMap(null);
                    }

                    var poly = new google.maps.Polyline({
                        path: points,
                        strokeColor: "#FF0000",
                        strokeOpacity: .7,
                        strokeWeight: 4
                    });

                    components.push(poly);

                    var imageStart = "../images/start.png";
                    var startMarker = new google.maps.Marker({
                        position: new google.maps.LatLng(arr_lat[0], arr_lon[0]),
                        map: map,
                        icon: imageStart
                    });

                    components.push(startMarker);

                    var imageStop = "../images/stop.png";
                    var stopMarker = new google.maps.Marker({
                        position: new google.maps.LatLng(arr_lat[arr_lat.length - 1], arr_lon[arr_lon.length - 1]),
                        map: map,
                        icon: imageStop
                    });

                    components.push(stopMarker);

                    poly.setMap(map);

                    map.fitBounds(bounds);
                };
            }

            reader.readAsText(file);
            
        });
}