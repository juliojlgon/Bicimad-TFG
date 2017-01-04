package com.bicis_tfg.bicimad_tfg_app.models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import javax.annotation.Generated;

@Generated("org.jsonschema2pojo")
public class History {

    @SerializedName("UserId")
    @Expose
    private String userId;
    @SerializedName("BikeId")
    @Expose
    private String bikeId;
    @SerializedName("ArrivalStationId")
    @Expose
    private String arrivalStationId;
    @SerializedName("ArrivalStationUserName")
    @Expose
    private String arrivalStationUserName;
    @SerializedName("DepartureStationId")
    @Expose
    private String departureStationId;
    @SerializedName("DepartureStationUserName")
    @Expose
    private String departureStationUserName;
    @SerializedName("Finished")
    @Expose
    private boolean finished;
    @SerializedName("Id")
    @Expose
    private String id;
    @SerializedName("CreatedDate")
    @Expose
    private String createdDate;

    /**
     *
     * @return
     * The userId
     */
    public String getUserId() {
        return userId;
    }

    /**
     *
     * @param userId
     * The UserId
     */
    public void setUserId(String userId) {
        this.userId = userId;
    }

    /**
     *
     * @return
     * The bikeId
     */
    public String getBikeId() {
        return bikeId;
    }

    /**
     *
     * @param bikeId
     * The BikeId
     */
    public void setBikeId(String bikeId) {
        this.bikeId = bikeId;
    }

    /**
     *
     * @return
     * The arrivalStationId
     */
    public String getArrivalStationId() {
        return arrivalStationId;
    }

    /**
     *
     * @param arrivalStationId
     * The ArrivalStationId
     */
    public void setArrivalStationId(String arrivalStationId) {
        this.arrivalStationId = arrivalStationId;
    }

    /**
     *
     * @return
     * The arrivalStationUserName
     */
    public String getArrivalStationUserName() {
        return arrivalStationUserName;
    }

    /**
     *
     * @param arrivalStationUserName
     * The ArrivalStationUserName
     */
    public void setArrivalStationUserName(String arrivalStationUserName) {
        this.arrivalStationUserName = arrivalStationUserName;
    }

    /**
     *
     * @return
     * The departureStationId
     */
    public String getDepartureStationId() {
        return departureStationId;
    }

    /**
     *
     * @param departureStationId
     * The DepartureStationId
     */
    public void setDepartureStationId(String departureStationId) {
        this.departureStationId = departureStationId;
    }

    /**
     *
     * @return
     * The departureStationUserName
     */
    public String getDepartureStationUserName() {
        return departureStationUserName;
    }

    /**
     *
     * @param departureStationUserName
     * The DepartureStationUserName
     */
    public void setDepartureStationUserName(String departureStationUserName) {
        this.departureStationUserName = departureStationUserName;
    }

    /**
     *
     * @return
     * The finished
     */
    public boolean isFinished() {
        return finished;
    }

    /**
     *
     * @param finished
     * The Finished
     */
    public void setFinished(boolean finished) {
        this.finished = finished;
    }

    /**
     *
     * @return
     * The id
     */
    public String getId() {
        return id;
    }

    /**
     *
     * @param id
     * The Id
     */
    public void setId(String id) {
        this.id = id;
    }

    /**
     *
     * @return
     * The createdDate
     */
    public String getCreatedDate() {
        return createdDate;
    }

    /**
     *
     * @param createdDate
     * The CreatedDate
     */
    public void setCreatedDate(String createdDate) {
        this.createdDate = createdDate;
    }


}