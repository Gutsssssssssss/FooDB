namespace FooDB;

public interface Iindex<K, V>
{
    void Insert(K key, V value);
    Tuple<K, V> Get(K key);
    IEnumerable<Tuple<K, V>> LargerThanOrEqualTo(K key);
    IEnumerable<Tuple<K, V>> LessThanOrEqualTo(K key);
    IEnumerable<Tuple<K, V>> LessThan(K key);
    bool Delete(K key, V value, IComparer<V> valueComparer = null);
    bool Delete(K key);
}