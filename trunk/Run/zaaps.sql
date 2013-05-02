DELETE FROM interactives_templates_skills WHERE InteractiveTemplateId=16;
DELETE FROM interactives_skills WHERE Type="ZaapTeleport";
INSERT INTO interactives_skills (Type, Duration, CustomTemplateId) VALUES ("ZaapTeleport", 0, 114);
INSERT INTO interactives_templates_skills (InteractiveTemplateId, SkillId) VALUES (16, LAST_INSERT_ID());
DELETE FROM interactives_skills WHERE Type="ZaapSave";
INSERT INTO interactives_skills (Type, Duration, CustomTemplateId) VALUES ("ZaapSave", 0, 44);
INSERT INTO interactives_templates_skills (InteractiveTemplateId, SkillId) VALUES (16, LAST_INSERT_ID());
DELETE FROM interactives_spawns WHERE TemplateId = 16;
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 458520, 54172489);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 456126, 54172969);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 1254, 13060);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 1246, 8991);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 443686, 154642);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 458282, 17932);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 159227, 15153);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 149940, 12054);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 60101, 15654);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 1261, 13605);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 475744, 84806401);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 475642, 100270593);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 52836, 14419207);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 437118, 141588);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 58211, 144419);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 472656, 88082704);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 443366, 133896);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 472655, 88212746);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 57534, 138543);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 461166, 73400320);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 57531, 800);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 472735, 88212481);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 472654, 88213271);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 463609, 138012);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 437117, 143372);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 463686, 80216580);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 465379, 84674563);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 463688, 80220164);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 463687, 80217096);
INSERT INTO interactives_spawns (TemplateId, ElementId, MapId) VALUES (16, 463689, 80219137);