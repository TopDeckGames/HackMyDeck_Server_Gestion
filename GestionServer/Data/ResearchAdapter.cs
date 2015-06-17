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
    }
}
