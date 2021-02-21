Select u.nome,u.email,r.Name from aspnetusers u
inner join AspNetUserRoles ur on ur.UserId = u.id
inner join AspNetroles] r on r.Id = ur.RoleId