package com.bicis_tfg.bicimad_tfg_app.models;

import net.danlew.android.joda.JodaTimeAndroid;

import org.joda.time.DateTime;

/**
 * Created by JulioLopez on 4/6/16.
 */
public class History {
    private String UserId;
    private String BikeId;
    private String ArrivalStationId;
    private String ArrivalStationName;
    private String DepartureStationId;
    private String DepartureStationName;
    private boolean Finished;
    private String Id;
    private DateTime CreatedAt;

    public History(String bikeId, String userId, String arrivalStationName, String arrivalStationId, String departureStationName, String departureStationId, boolean finished, String id, DateTime createdAt) {
        setBikeId(bikeId);
        setUserId(userId);
        setArrivalStationName(arrivalStationName);
        setArrivalStationId(arrivalStationId);
        setDepartureStationName(departureStationName);
        setDepartureStationId(departureStationId);
        setFinished(finished);
        setId(id);
        setCreatedAt(createdAt);
    }

    public String getUserId() {
        return UserId;
    }

    public void setUserId(String userId) {
        UserId = userId;
    }

    public String getBikeId() {
        return BikeId;
    }

    public void setBikeId(String bikeId) {
        BikeId = bikeId;
    }

    public String getArrivalStationId() {
        return ArrivalStationId;
    }

    public void setArrivalStationId(String arrivalStationId) {
        ArrivalStationId = arrivalStationId;
    }

    public String getArrivalStationName() {
        return ArrivalStationName;
    }

    public void setArrivalStationName(String arrivalStationName) {
        ArrivalStationName = arrivalStationName;
    }

    public String getDepartureStationId() {
        return DepartureStationId;
    }

    public void setDepartureStationId(String departureStationId) {
        DepartureStationId = departureStationId;
    }

    public String getDepartureStationName() {
        return DepartureStationName;
    }

    public void setDepartureStationName(String departureStationName) {
        DepartureStationName = departureStationName;
    }

    public boolean isFinished() {
        return Finished;
    }

    public void setFinished(boolean finished) {
        Finished = finished;
    }

    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }

    public DateTime getCreatedAt() {
        return CreatedAt;
    }

    public void setCreatedAt(DateTime createdAt) {
        CreatedAt = createdAt;
    }
}
