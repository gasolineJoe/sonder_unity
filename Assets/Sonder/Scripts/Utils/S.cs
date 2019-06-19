namespace DefaultNamespace {
    public static class S {
        public static float BoundValue(float value, float bound) {
            return BoundValue(value, -bound, bound);
        }

        public static float BoundValue(float value, float lower, float upper) {
            return value > upper ? upper : value < lower ? lower : value;
        }
    }
}