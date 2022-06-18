

namespace WebStore.Interfaces.TestAPI;

public interface IValuesService
{
    IEnumerable<string> GetValues();

    string GetById(int Id);

    void Add(string value);

    void Edit(int Id, string value);

    bool Delete(int Id);
}
