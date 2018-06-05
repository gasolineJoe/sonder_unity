using LeopotamGroup.Ecs;

public class EcsSonderGameWorld : EcsWorld
{
    public readonly SonderStartupData startupData;

    public EcsSonderGameWorld(SonderStartupData data)
    {
        startupData = data;
    }
}