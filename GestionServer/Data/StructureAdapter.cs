using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using GestionServer.Model;

namespace GestionServer.Data
{
    public class StructureAdapter : BaseAdapter
    {
        /// <summary>
        /// Récupère la liste des structures du jeu
        /// </summary>
        /// <returns>Liste de structures</returns>
        public List<Structure> getStructures()
        {
            List<Structure> structures = new List<Structure>();

            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT `id`, `name`, `type`, `description` FROM `structure`";

            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Structure structure = new Structure();
                            structure.Id = (int)reader["id"];
                            structure.Name = (string)reader["name"];
                            structure.Description = (string)reader["description"];
                            structure.Type = (Structure.TypeBat)reader["type"];

                            structures.Add(structure);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                base.connection.Close();
            }

            return structures;
        }

        /// <summary>
        /// Récupère le détail des structures liées à l'utilisateur
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <returns>Liste des structures</returns>
        public List<UserStructure> getUserStructures(int idUser)
        {
            List<UserStructure> structures = new List<UserStructure>();

            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT `id`, `structure_id`, `effectif`, `level`, `locked` FROM `user_structure` WHERE user_id = @idUser";
            cmd.Parameters.AddWithValue("@idUser", idUser);

            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserStructure structure = new UserStructure();
                            structure.IdStructure = (int)reader["structure_id"];
                            structure.Level = (int)reader["level"];
                            structure.Locked = (bool)reader["locked"];
                            structure.Effectif = (int)reader["effectif"];

                            structures.Add(structure);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                base.connection.Close();
            }

            return structures;
        }
    }
}
