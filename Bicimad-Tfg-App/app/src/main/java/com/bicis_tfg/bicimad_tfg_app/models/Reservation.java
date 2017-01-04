
package com.bicis_tfg.bicimad_tfg_app.models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import javax.annotation.Generated;

@Generated("org.jsonschema2pojo")
public class Reservation {

    @SerializedName("Isbike")
    @Expose
    private boolean isbike;
    @SerializedName("UserId")
    @Expose
    private String userId;
    @SerializedName("ItemId")
    @Expose
    private String itemId;
    @SerializedName("StationId")
    @Expose
    private String stationId;
    @SerializedName("StationName")
    @Expose
    private String stationName;
    @SerializedName("Id")
    @Expose
    private String id;
    @SerializedName("CreatedDate")
    @Expose
    private String createdDate;

    /**
     * @return The isbike
     */
    public boolean isIsbike() {
        return isbike;
    }

    /**
     * @param isbike The Isbike
     */
    public void setIsbike(boolean isbike) {
        this.isbike = isbike;
    }

    /**
     * @return The userId
     */
    public String getUserId() {
        return userId;
    }

    /**
     * @param userId The UserId
     */
    public void setUserId(String userId) {
        this.userId = userId;
    }

    /**
     * @return The itemId
     */
    public String getItemId() {
        return itemId;
    }

    /**
     * @param itemId The ItemId
     */
    public void setItemId(String itemId) {
        this.itemId = itemId;
    }

    /**
     * @return The stationId
     */
    public String getStationId() {
        return stationId;
    }

    /**
     * @param stationId The StationId
     */
    public void setStationId(String stationId) {
        this.stationId = stationId;
    }

    /**
     * @return The stationName
     */
    public String getStationName() {
        return stationName;
    }

    /**
     * @param stationName The StationName
     */
    public void setStationName(String stationName) {
        this.stationName = stationName;
    }

    /**
     * @return The id
     */
    public String getId() {
        return id;
    }

    /**
     * @param id The Id
     */
    public void setId(String id) {
        this.id = id;
    }

    /**
     * @return The createdDate
     */
    public String getCreatedDate() {
        return createdDate;
    }

    /**
     * @param createdDate The CreatedDate
     */
    public void setCreatedDate(String createdDate) {
        this.createdDate = createdDate;
    }

}