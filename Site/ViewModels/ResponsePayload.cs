namespace Site.ViewModels;
public record ResponsePayload(bool Succeeded, string Message);

public record ResponsePayload<T>(bool Succeeded, string Message , T Obj);