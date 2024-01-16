namespace ArchitectureLibrary
{
    public interface IInvokeable { void Invoke(); void Cancel() { } }
    public interface IInvokeable<T> { void Invoke(T value); void Cancel() { } }
    public interface IActivateable { bool isActive { get; set; } }
    public interface IResetable { void Reset(); }
}