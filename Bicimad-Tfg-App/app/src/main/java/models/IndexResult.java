package models;


    import java.util.HashMap;
    import java.util.Map;

    public class IndexResult {

        private Integer BrokenBikes;
        private Integer FreeBikes;
        private Integer ActiveBikes;
        private Map<String, Object> additionalProperties = new HashMap<String, Object>();

        /**
         *
         * @return
         * The BrokenBikes
         */
        public Integer getBrokenBikes() {
            return BrokenBikes;
        }

        /**
         *
         * @param BrokenBikes
         * The BrokenBikes
         */
        public void setBrokenBikes(Integer BrokenBikes) {
            this.BrokenBikes = BrokenBikes;
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
         * The ActiveBikes
         */
        public Integer getActiveBikes() {
            return ActiveBikes;
        }

        /**
         *
         * @param ActiveBikes
         * The ActiveBikes
         */
        public void setActiveBikes(Integer ActiveBikes) {
            this.ActiveBikes = ActiveBikes;
        }

        public Map<String, Object> getAdditionalProperties() {
            return this.additionalProperties;
        }

        public void setAdditionalProperty(String name, Object value) {
            this.additionalProperties.put(name, value);
        }

    }

