function myMap() {
    $('#errorlat').hide();
    $('#errorlng').hide();

    var mapProp = {
        center: new google.maps.LatLng(55.0944, 61.2411),
        zoom: 7,
    };
    var map = new google.maps.Map(document.getElementById('googleMap'), mapProp);

    var input = (document.getElementById('map-search'));
    
    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    var marker = new google.maps.Marker({
        map: map,
        anchorPoint: new google.maps.Point(0, -29)
    });

    autocomplete.addListener('place_changed', function () {
        marker.setVisible(false);
        var place = autocomplete.getPlace();
        
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17); 
        }
        marker.setIcon(({
            url: place.icon,
            size: new google.maps.Size(71, 71),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(35, 35)
        }));
        marker.setPosition(place.geometry.location);
        marker.setVisible(true);
    });

    $("#btnsearch").click(
        function () {
            var lat = $("#lat").val();
            var lng = $("#lng").val();
            var flag = true;
            if (!$.trim(lat) || isNaN(parseFloat(lat)) || !isFinite(lat)) {
                $('#errorlat').show();
                flag = false;
            }
            else {
                $('#errorlat').hide();
            }
            if (!$.trim(lng) || isNaN(parseFloat(lng)) || !isFinite(lng)) {
                $('#errorlng').show();
                flag = false;
            }
            else {
                $('#errorlng').hide();
            }
            if (flag === true) {

                var latlng = new google.maps.LatLng(lat, lng);
                map.setCenter(latlng);
                map.setZoom(10); 
                marker.setPosition(latlng);
                marker.setVisible(true);
            }
        }
    );
}