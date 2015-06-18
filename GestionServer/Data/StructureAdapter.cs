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
            cmd.CommandText = "SELECT `id`, `name`, `type`, `description`, `posX`, `posY`, `width`, `height` FROM `structure`";

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
                            structure.PosX = (double)reader["posX"];
                            structure.PosY = (double)reader["posY"];
                            structure.Width = (double)reader["width"];
                            structure.Height = (double)reader["height"];

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
                            structure.id = (int)reader["structure_id"];
                            structure.level = (int)reader["level"];
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

        public void lvlUp(int idUser, int idStruct)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "UPDATE `user_structure` SET `level`= level + 1 WHERE user_id = @idUser AND structure_id = @idStruct";
            cmd.Parameters.AddWithValue("@idUser", idUser);
            cmd.Parameters.AddWithValue("@idStruct", idStruct);

            try
            {
                base.connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
            }

            catch
            {
                throw;
            }
            finally
            {
                base.connection.Close();
            }
        }
    }
}
