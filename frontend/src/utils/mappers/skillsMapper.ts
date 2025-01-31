interface Skill {
  name: string;
  level: number;
  experience: number;
  rank: number;
}

export function transformSkills(skills: Skill[]): Skill[] {
  return skills.map((skill: Skill) => ({
    name: skill.name,
    level: skill.level,
    experience: skill.experience,
    rank: skill.rank,
  }));
}
