namespace Sonder.Scripts.Components.World.Entities {
    public abstract class BaseEntity {
        protected int Entity;

        protected static T CreateThis<T>(EcsSonderGameWorld world) where T : BaseEntity, new() {
            var entity = world.CreateEntity();
            var result = world.AddComponent<T>(entity);
            result.Entity = entity;
            return result;
        }

        protected T AddComponent<T>(EcsSonderGameWorld world) where T : class, new() {
            return world.AddComponent<T>(Entity);
        }
    }
}