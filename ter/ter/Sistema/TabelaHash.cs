using System;
using System.Collections.Generic;

namespace Sistema
{
    public class TabelaHash<TValor>
    {
        private readonly int capacidade;
        private readonly LinkedList<KeyValuePair<string, TValor>>[] buckets;

        public TabelaHash(int capacidade = 10)
        {
            this.capacidade = capacidade;
            buckets = new LinkedList<KeyValuePair<string, TValor>>[capacidade];
            for (int i = 0; i < capacidade; i++)
                buckets[i] = new LinkedList<KeyValuePair<string, TValor>>();
        }

        private int Hash(string chave)
        {
            return Math.Abs(chave.GetHashCode()) % capacidade;
        }

        public void Inserir(string chave, TValor valor)
        {
            int idx = Hash(chave);
            foreach (var par in buckets[idx])
            {
                if (par.Key == chave)
                    throw new Exception("CPF já cadastrado.");
            }
            buckets[idx].AddLast(new KeyValuePair<string, TValor>(chave, valor));
        }

        public TValor Buscar(string chave)
        {
            int idx = Hash(chave);
            foreach (var par in buckets[idx])
            {
                if (par.Key == chave)
                    return par.Value;
            }
            throw new Exception("Paciente não encontrado.");
        }

        public void Atualizar(string chave, TValor novoValor)
        {
            int idx = Hash(chave);
            var node = buckets[idx].First;
            while (node != null)
            {
                if (node.Value.Key == chave)
                {
                    node.Value = new KeyValuePair<string, TValor>(chave, novoValor);
                    return;
                }
                node = node.Next;
            }
            throw new Exception("Paciente não encontrado.");
        }

        public void Remover(string chave)
        {
            int idx = Hash(chave);
            var node = buckets[idx].First;
            while (node != null)
            {
                if (node.Value.Key == chave)
                {
                    buckets[idx].Remove(node);
                    return;
                }
                node = node.Next;
            }
            throw new Exception("Paciente não encontrado.");
        }

        public IEnumerable<KeyValuePair<string, TValor>> Todos()
        {
            foreach (var bucket in buckets)
                foreach (var par in bucket)
                    yield return par;
        }

        public LinkedList<KeyValuePair<string, TValor>>[] Buckets => buckets;
    }
}
