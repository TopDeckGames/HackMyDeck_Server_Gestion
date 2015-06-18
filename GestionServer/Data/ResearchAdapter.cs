using GestionServer.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionServer.Data
{
    public class ResearchAdapter : BaseAdapter
    {
        /// <summary>
        /// Récupère la liste des skilltrees du jeu
        /// </summary>
        /// <returns></returns>
        public List<SkillTrees> getSkillTrees()
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT s.* FROM user u INNER JOIN user_skilltree s ON u.id = user_id INNER JOIN skilltree sk ON s.skill_id = sk.id";

            List<SkillTrees> skill = new List<SkillTrees>();
            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SkillTrees s = new SkillTrees();
                            s.id = (int)reader["id"];
                            s.label = (string)reader["label"];
                            s.type = (int)reader["type"];
                            skill.Add(s);
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
            return skill;
        }

        /// <summary>
        /// Récupère la liste des skilltrees associés à l'utilisateur
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public List<UserSkillTrees> getUserSkillTrees(int idUser)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT sk.* FROM user u INNER JOIN user_skilltree s ON u.id = user_id INNER JOIN skilltree sk ON s.skill_id = sk.id WHERE u.id = @idUser";
            cmd.Parameters.AddWithValue("@idUser", idUser);

            List<UserSkillTrees> skill = new List<UserSkillTrees>();
            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserSkillTrees user = new UserSkillTrees();
                            user.id = (int)reader["id"];
                            user.user_id = (int)reader["user_id"];
                            user.skill_id = (int)reader["skill_id"];
                            user.created = (DateTime)reader["created"];
                            user.updated = (DateTime)reader["updated"];
                            user.unlocked = (Boolean)reader["unlocked"];
                            user.effectif_allocated = (int)reader["effectif_allocated"];
                            user.lastEnhancement_id = (int)reader["lastEnhancement_id"];
                            skill.Add(user);
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
            return skill;
        }
<<<<<<< HEAD
        
        public List<Enhancements> getEnhancements()
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT en.* FROM user u INNER JOIN user_enhancement e ON u.id = user_id INNER JOIN enhancement en ON e.skill_id = en.id";

            List<Enhancements> enhancement = new List<Enhancements>();
=======

        /// <summary>
        /// Teste si le parent d'un enhancement est débloqué
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idEnhancement">Identifiant enhancement</param>
        /// <returns>Débloqué</returns>
        public bool isParentEnhancementUnlocked(int idUser, int idEnhancement)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText =   "SELECT unlocked                                                                                                            " +
                                "FROM user_enhancement                                                                                                      " +
                                "WHERE user_enhancement.user_id = @idUser                                                                                   " +
                                "AND user_enhancement.enhancement_id = (SELECT enhancement.parent_id FROM enhancement WHERE enhancement.id = @idEnhancement)";
            cmd.Parameters.AddWithValue("@idUser", idUser);
            cmd.Parameters.AddWithValue("@idEnhancement", idEnhancement);

>>>>>>> origin/master
            try
            {
                base.connection.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
<<<<<<< HEAD
                        while (reader.Read())
                        {
                            Enhancement e = new Enhancement();
                            e.id = (int)reader["id"];
                            e.name = (string)reader["name"];
                            e.description = (string)reader["description"];
                            e.effect = (string)reader["effect"];
                            e.cost = (int)reader["cost"];
                            e.time = (int)reader["time"];
                            e.parent = (int)reader["parent"];
                            e.skilltree = (int)reader["skilltree"];
                            
                            enhancement.Add(e);
                        }
=======
                        reader.Read();
                        return (bool)reader["unlocked"];
>>>>>>> origin/master
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
<<<<<<< HEAD
            return skill;
        }

        public List<UserEnhancements> getUserEnhancements(int idUser)
        {
            MySqlCommand cmd = base.connection.CreateCommand();
            cmd.CommandText = "SELECT e.* FROM user u INNER JOIN user_enhancement e ON u.id = user_id INNER JOIN enhancement en ON e.enhancement_id = en.id WHERE u.id = @idUser";
            cmd.Parameters.AddWithValue("@idUser", idUser);

            List<UserEnhancements> userEnhancement = new List<UserEnhancements>();
            try
            {
                base.connection.Open();
=======

            return false;
        }

        /// <summary>
        /// Définit une recherche comme étant la recherche en cours
        /// </summary>
        /// <param name="idUser">Identifiant de l'utilisateur</param>
        /// <param name="idEnhancement">Identifiant de la recherche</param>
        public void setCurrentResearch(int idUser, int idEnhancement)
        {
            try
            {
                base.connection.Open();

                //On vérifie si l'association existe déjà
                MySqlCommand cmd = base.connection.CreateCommand();
                cmd.CommandText = "SELECT count(*) as nb FROM user_enhancement WHERE user_id = @userId AND enhancement_id = @enhancementId";
                cmd.Parameters.AddWithValue("@userId", idUser);
                cmd.Parameters.AddWithValue("@enhancementId", idEnhancement);

                bool exist = false;
>>>>>>> origin/master
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
<<<<<<< HEAD
                        while (reader.Read())
                        {
                            UserSkillTrees user = new UserSkillTrees();
                            user.id = (int)reader["id"];
                            user.user_id = (int)reader["user_id"];
                            user.enhancement_id = (int)reader["enhancement_id"];
                            user.created = (DateTime)reader["created"];
                            user.updated = (DateTime)reader["updated"];
                            user.unlocked = (Boolean)reader["unlocked"];
                            user.on_current_research = (int)reader["on_current_research"];
                            
                            userEnhancement.Add(user);
                        }
                    }
                }
=======
                        reader.Read();
                        exist = (Int64)reader["nb"] > 0;
                    }
                }

                //On set la nouvelle recherche
                if(exist)
                {
                    cmd.CommandText = "INSERT INTO user_enhancement(user_id, enhancement_id, unlocked, on_current_research) VALUES (@userId, @enhancementId, 0, 1)";
                }
                else
                {
                    cmd.CommandText = "UPDATE user_enhancement SET on_current_research = 1 WHERE user_id = @userId AND enhancement_id = @enhancementId";
                }
                cmd.ExecuteNonQuery();

                //On enlève l'ancienne recherche
                cmd.CommandText = "UPDATE user_enhancement SET on_current_research = 0 WHERE user_id = @userId AND enhancement_id <> @enhancementId AND on_current_research = 1";
                cmd.ExecuteNonQuery();
>>>>>>> origin/master
            }
            catch
            {
                throw;
            }
            finally
            {
                base.connection.Close();
<<<<<<< HEAD
            }
            return skill;
=======
            }            
>>>>>>> origin/master
        }
    }
}
