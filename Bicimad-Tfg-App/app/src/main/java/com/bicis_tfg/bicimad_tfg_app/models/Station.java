package com.bicis_tfg.bicimad_tfg_app.models;

import java.util.HashMap;
import java.util.Map;

public class Station {

    private String Id;
    private String CreatedDate;
    private Integer BikeNum;
    private Integer FreeBikes;
    private String StationName;
    private String FriendlyUrlStationName;
    private String Latitude;
    private String Longitude;
    private String Metro;
    private String Bus;
    private Integer ReservedSlots;
    private Boolean IsBikeBooked;
    private Boolean IsSlotBooked;
    private Integer AvailSlots;
    private Map<String, Object> additionalProperties = new HashMap<String, Object>();

    /**
     *
     * @return
     * The Id
     */
    public String getId() {
        return Id;
    }

    /**
     *
     * @param Id
     * The Id
     */
    public void setId(String Id) {
        this.Id = Id;
    }

    /**
     *
     * @return
     * The CreatedDate
     */
    public String getCreatedDate() {
        return CreatedDate;
    }

    /**
     *
     * @param CreatedDate
     * The CreatedDate
     */
    public void setCreatedDate(String CreatedDate) {
        this.CreatedDate = CreatedDate;
    }

    /**
     *
     * @return
     * The BikeNum
     */
    public Integer getBikeNum() {
        return BikeNum;
    }

    /**
     *
     * @param BikeNum
     * The BikeNum
     */
    public void setBikeNum(Integer BikeNum) {
        this.BikeNum = BikeNum;
    }

    /**
     *
     * @return
     * The FreeBikes
     */
    public Integer getFreeBikes() {
        return FreeBikes;
    }

    /**
     *
     * @param FreeBikes
     * The FreeBikes
     */
    public void setFreeBikes(Integer FreeBikes) {
        this.FreeBikes = FreeBikes;
    }

    /**
     *
     * @return
     * The StationName
     */
    public String getStationName() {
        return StationName;
    }

    /**
     *
     * @param StationName
     * The StationName
     */
    public void setStationName(String StationName) {
        this.StationName = StationName;
    }

    /**
     *
     * @return
     * The FriendlyUrlStationName
     */
    public String getFriendlyUrlStationName() {
        return FriendlyUrlStationName;
    }

    /**
     *
     * @param FriendlyUrlStationName
     * The FriendlyUrlStationName
     */
    public void setFriendlyUrlStationName(String FriendlyUrlStationName) {
        this.FriendlyUrlStationName = FriendlyUrlStationName;
    }

    /**
     *
     * @return
     * The Latitude
     */
    public String getLatitude() {
        return Latitude;
    }

    /**
     *
     * @param Latitude
     * The Latitude
     */
    public void setLatitude(String Latitude) {
        this.Latitude = Latitude;
    }

    /**
     *
     * @return
     * The Longitude
     */
    public String getLongitude() {
        return Longitude;
    }

    /**
     *
     * @param Longitude
     * The Longitude
     */
    public void setLongitude(String Longitude) {
        this.Longitude = Longitude;
    }

    /**
     *
     * @return
     * The Metro
     */
    public String getMetro() {
        return Metro;
    }

    /**
     *
     * @param Metro
     * The Metro
     */
    public void setMetro(String Metro) {
        this.Metro = Metro;
    }

    /**
     *
     * @return
     * The Bus
     */
    public String getBus() {
        return Bus;
    }

    /**
     *
     * @param Bus
     * The Bus
     */
    public void setBus(String Bus) {
        this.Bus = Bus;
    }

    /**
     *
     * @return
     * The ReservedSlots
     */
    public Integer getReservedSlots() {
        return ReservedSlots;
    }

    /**
     *
     * @param ReservedSlots
     * The ReservedSlots
     */
    public void setReservedSlots(Integer ReservedSlots) {
        this.ReservedSlots = ReservedSlots;
    }

    /**
     *
     * @return
     * The IsBikeBooked
     */
    public Boolean getIsBikeBooked() {
        return IsBikeBooked;
    }

    /**
     *
     * @param IsBikeBooked
     * The IsBikeBooked
     */
    public void setIsBikeBooked(Boolean IsBikeBooked) {
        this.IsBikeBooked = IsBikeBooked;
    }

    /**
     *
     * @return
     * The IsSlotBooked
     */
    public Boolean getIsSlotBooked() {
        return IsSlotBooked;
    }

    /**
     *
     * @param IsSlotBooked
     * The IsSlotBooked
     */
    public void setIsSlotBooked(Boolean IsSlotBooked) {
        this.IsSlotBooked = IsSlotBooked;
    }

    /**
     *
     * @return
     * The AvailSlots
     */
    public Integer getAvailSlots() {
        return AvailSlots;
    }

    /**
     *
     * @param AvailSlots
     * The AvailSlots
     */
    public void setAvailSlots(Integer AvailSlots) {
        this.AvailSlots = AvailSlots;
    }

    public Map<String, Object> getAdditionalProperties() {
        return this.additionalProperties;
    }

    public void setAdditionalProperty(String name, Object value) {
        this.additionalProperties.put(name, value);
    }

}
